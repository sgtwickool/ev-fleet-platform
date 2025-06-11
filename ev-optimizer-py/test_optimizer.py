# test_optimizer.py
"""
Test the optimize_vehicle_routes function with mock data for EV fleet routing.
"""
from optimizer import optimize_vehicle_routes

def main():
    # Mock distance matrix (4 locations)
    distance_matrix = [
        [0, 10, 15, 20],
        [10, 0, 35, 25],
        [15, 35, 0, 30],
        [20, 25, 30, 0]
    ]
    num_vehicles = 2
    depots = [0]  # depot at node 0
    vehicles = [
        {"id": "veh1", "soc": 100, "max_range": 100, "location": 0},
        {"id": "veh2", "soc": 100, "max_range": 100, "location": 0}
    ]
    deliveries = [
        {"id": "del1", "location": 1, "urgency": "high", "time_window": [0, 100]},
        {"id": "del2", "location": 2, "urgency": "low", "time_window": [0, 100]},
        {"id": "del3", "location": 3, "urgency": "medium", "time_window": [0, 100]}
    ]
    charging_stations = [
        {"location": 0, "cost": {"peak": 0.30, "night": 0.10}, "slots": 2},
        {"location": 3, "cost": {"variable": [["Mon", 0.25], ["Tue", 0.20]]}, "slots": 1}
    ]
    drivers = [
        {"id": "drv1", "available": True},
        {"id": "drv2", "available": True}
    ]

    result = optimize_vehicle_routes(
        distance_matrix,
        num_vehicles,
        depots,
        vehicles,
        deliveries,
        charging_stations,
        drivers
    )
    print("Optimization result:")
    if 'routes' in result:
        for i, route in enumerate(result['routes']):
            print(f"\nVehicle {i+1} route:")
            for step in route:
                loc = step['location']
                soc = step['soc']
                is_cs = step['is_charging_station']
                cs_str = ' (charging station)' if is_cs else ''
                print(f"  Location {loc}{cs_str}, SoC: {soc}")
        print(f"\nMessage: {result['message']}")
    else:
        print(result)

if __name__ == "__main__":
    main()
