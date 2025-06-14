"""
constraints.py
Constraint helpers for EV fleet optimizer (SoC, charging, time windows, etc).
"""

import logging
from typing import List, Dict, Any
from ortools.constraint_solver import pywrapcp


def add_distance_dimension(
    routing: pywrapcp.RoutingModel,
    manager: pywrapcp.RoutingIndexManager,
    distance_matrix: List[List[int]],
    max_ranges: List[float],
) -> pywrapcp.RoutingDimension:
    """
    Adds a distance dimension to the routing model to represent SoC/battery usage.
    """

    def distance_callback(from_index, to_index):
        from_node = manager.IndexToNode(from_index)
        to_node = manager.IndexToNode(to_index)
        return distance_matrix[from_node][to_node]

    transit_callback_index = routing.RegisterTransitCallback(distance_callback)
    routing.SetArcCostEvaluatorOfAllVehicles(transit_callback_index)
    routing.AddDimension(
        transit_callback_index,
        0,  # no slack
        int(max(max_ranges)),  # max distance any vehicle can travel
        True,  # start cumul to zero
        "Distance",
    )
    distance_dimension = routing.GetDimensionOrDie("Distance")
    for vehicle_id, max_range in enumerate(max_ranges):
        distance_dimension.CumulVar(routing.End(vehicle_id)).SetMax(int(max_range))
    return distance_dimension


def add_charging_constraints(
    distance_dimension: pywrapcp.RoutingDimension,
    manager: pywrapcp.RoutingIndexManager,
    charging_stations: List[Dict[str, Any]],
    max_ranges: List[float],
):
    """
    Allows recharging at charging station nodes by setting slack.
    """
    charging_locations = set(cs["location"] for cs in charging_stations)
    for node in charging_locations:
        index = manager.NodeToIndex(node)
        distance_dimension.SlackVar(index).SetMax(int(max(max_ranges)))


def add_time_windows(
    routing: pywrapcp.RoutingModel,
    manager: pywrapcp.RoutingIndexManager,
    distance_matrix: List[List[int]],
    deliveries: List[Dict[str, Any]],
    depots,
    time_limit: int = 10000,
):
    """
    Adds time window constraints to the routing model.
    """
    if not any("time_window" in d for d in deliveries):
        return None
    time_windows = [d.get("time_window", [0, time_limit]) for d in deliveries]
    for depot in depots if isinstance(depots, list) else [depots]:
        time_windows.insert(depot, [0, time_limit])

    def time_callback(from_index, to_index):
        from_node = manager.IndexToNode(from_index)
        to_node = manager.IndexToNode(to_index)
        return distance_matrix[from_node][to_node]

    time_callback_index = routing.RegisterTransitCallback(time_callback)
    routing.AddDimension(
        time_callback_index,
        time_limit,  # allow waiting
        time_limit,  # max time per vehicle
        False,
        "Time",
    )
    time_dimension = routing.GetDimensionOrDie("Time")
    for location_idx, window in enumerate(time_windows):
        index = manager.NodeToIndex(location_idx)
        time_dimension.CumulVar(index).SetRange(window[0], window[1])
    return time_dimension


def restrict_unavailable_vehicles(
    routing: pywrapcp.RoutingModel, drivers: List[Dict[str, Any]], num_vehicles: int
):
    """
    Restricts assignment of routes to unavailable vehicles.
    """
    available_vehicles = [
        i for i, drv in enumerate(drivers) if drv.get("available", True)
    ]
    for vehicle_id in range(num_vehicles):
        if vehicle_id not in available_vehicles:
            routing.VehicleVar(routing.Start(vehicle_id)).RemoveValue(vehicle_id)
