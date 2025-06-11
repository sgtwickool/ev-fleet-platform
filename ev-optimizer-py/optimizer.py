# optimizer.py
# Contains the optimization logic for EV fleet routing.

from ortools.constraint_solver import pywrapcp, routing_enums_pb2


def optimize_vehicle_routes(
    distance_matrix,
    num_vehicles,
    depots,
    vehicles,
    deliveries,
    charging_stations,
    drivers,
):
    """
    Optimize vehicle routes based on the provided distance matrix and advanced payload.
    This function implements a basic VRP using ortools. Advanced constraints (SOC, time windows, etc.)
    can be added as needed.
    """
    # Handle depots: OR-Tools expects int for single depot, or two lists for multiple starts/ends
    if isinstance(depots, list) and len(depots) == 1:
        depots = depots[0]
    manager = pywrapcp.RoutingIndexManager(len(distance_matrix), num_vehicles, depots)

    # Create Routing Model
    routing = pywrapcp.RoutingModel(manager)

    # --- Advanced Constraints: SoC and Charging Stations ---
    # For each vehicle, set its max distance (based on SoC and max_range)
    max_ranges = [v.get("soc", 100) / 100.0 * v.get("max_range", 200) for v in vehicles]

    # Add a dimension for distance (to model battery usage)
    def distance_callback(from_index, to_index):
        from_node = manager.IndexToNode(from_index)
        to_node = manager.IndexToNode(to_index)
        return distance_matrix[from_node][to_node]

    transit_callback_index = routing.RegisterTransitCallback(distance_callback)
    routing.SetArcCostEvaluatorOfAllVehicles(transit_callback_index)
    routing.AddDimension(
        transit_callback_index,
        0,  # no slack
        int(max(max_ranges)),  # maximum distance any vehicle can travel
        True,  # start cumul to zero
        "Distance",
    )
    distance_dimension = routing.GetDimensionOrDie("Distance")
    # Set vehicle-specific max distances (battery limits)
    for vehicle_id, max_range in enumerate(max_ranges):
        distance_dimension.CumulVar(routing.End(vehicle_id)).SetMax(int(max_range))

    # --- Charging Stations: allow recharging at certain nodes ---
    charging_locations = set(cs["location"] for cs in charging_stations)
    # For each node, if it's a charging station, allow a 'recharge' (reset SoC)
    # This is a simplification: in real use, you'd model time/cost for charging
    # Here, we allow the distance dimension to reset at charging stations
    for node in charging_locations:
        index = manager.NodeToIndex(node)
        distance_dimension.SlackVar(index).SetMax(int(max(max_ranges)))

    # --- Time Windows (if present in deliveries) ---
    if any("time_window" in d for d in deliveries):
        # Assume all locations have a time window, default to [0, 10000]
        time_windows = [d.get("time_window", [0, 10000]) for d in deliveries]
        # Add depot time window
        for depot in depots if isinstance(depots, list) else [depots]:
            time_windows.insert(depot, [0, 10000])

        def time_callback(from_index, to_index):
            from_node = manager.IndexToNode(from_index)
            to_node = manager.IndexToNode(to_index)
            return distance_matrix[from_node][
                to_node
            ]  # or use travel time if available

        time_callback_index = routing.RegisterTransitCallback(time_callback)
        routing.AddDimension(
            time_callback_index,
            10000,  # allow waiting
            10000,  # max time per vehicle
            False,
            "Time",
        )
        time_dimension = routing.GetDimensionOrDie("Time")
        for location_idx, window in enumerate(time_windows):
            index = manager.NodeToIndex(location_idx)
            time_dimension.CumulVar(index).SetRange(window[0], window[1])

    # --- Driver Availability (if present) ---
    if drivers:
        available_vehicles = [
            i for i, drv in enumerate(drivers) if drv.get("available", True)
        ]
        # Only assign routes to available vehicles
        for vehicle_id in range(num_vehicles):
            if vehicle_id not in available_vehicles:
                routing.VehicleVar(routing.Start(vehicle_id)).RemoveValue(vehicle_id)

    # --- Solve and Extract Solution ---
    search_parameters = pywrapcp.DefaultRoutingSearchParameters()
    search_parameters.first_solution_strategy = (
        routing_enums_pb2.FirstSolutionStrategy.PATH_CHEAPEST_ARC
    )
    search_parameters.local_search_metaheuristic = (
        routing_enums_pb2.LocalSearchMetaheuristic.GUIDED_LOCAL_SEARCH
    )
    search_parameters.time_limit.seconds = 10

    solution = routing.SolveWithParameters(search_parameters)

    routes = []
    if solution:
        for vehicle_id in range(num_vehicles):
            index = routing.Start(vehicle_id)
            route = []
            soc = max_ranges[vehicle_id]
            while not routing.IsEnd(index):
                node = manager.IndexToNode(index)
                route.append(
                    {
                        "location": node,
                        "soc": soc,
                        "is_charging_station": node in charging_locations,
                    }
                )
                next_index = solution.Value(routing.NextVar(index))
                distance = (
                    distance_matrix[node][manager.IndexToNode(next_index)]
                    if not routing.IsEnd(next_index)
                    else 0
                )
                soc -= distance
                if node in charging_locations:
                    soc = max_ranges[vehicle_id]  # recharge
                index = next_index
            route.append(
                {
                    "location": manager.IndexToNode(index),
                    "soc": soc,
                    "is_charging_station": False,
                }
            )
            routes.append(route)
        return {
            "routes": routes,
            "message": "Optimization with SoC and charging constraints successful.",
        }
    else:
        return {
            "message": "No solution found.",
        }
