# Example JSON payload for /optimize-trip:
# {
#   "distance_matrix": [
#     [0, 20, 18, 12],
#     [20, 0, 30, 14],
#     [18, 30, 0, 22],
#     [12, 14, 22, 0]
#   ],
#   "num_vehicles": 2,
#   "depot": 0
# }
#
# - distance_matrix: 2D array, where [i][j] is the distance from location i to j.
# - num_vehicles: number of vehicles in the fleet.
# - depot: index of the depot in the distance matrix (usually 0).
#
# You can POST this JSON to /optimize-trip to get optimized routes.
#
# Example advanced JSON payload for /optimize-trip:
# {
#   "distance_matrix": [[0, 20, 18, 12], [20, 0, 30, 14], [18, 30, 0, 22], [12, 14, 22, 0]],
#   "num_vehicles": 2,, data["hints"]
#   "depot": 0,
#   "vehicles": [
#     {"id": "veh1", "soc": 80, "max_range": 250, "location": 0},
#     {"id": "veh2", "soc": 60, "max_range": 180, "location": 0}
#   ],
#   "deliveries": [
#     {"id": "del1", "location": 1, "urgency": "high", "time_window": [8, 10]},
#     {"id": "del2", "location": 2, "urgency": "low", "time_window": [9, 17]},
#     {"id": "del3", "location": 3, "urgency": "medium", "time_window": [12, 16]}
#   ],
#   "charging_stations": [
#     {"location": 0, "cost": {"peak": 0.30, "night": 0.10}, "slots": 2},
#     {"location": 3, "cost": {"variable": [["Mon", 0.25], ["Tue", 0.20]]}, "slots": 1}
#   ],
#   "drivers": [
#     {"id": "drv1", "available": true},
#     {"id": "drv2", "available": false}
#   ]
# }
#
# - vehicles: list of vehicles with state of charge (soc), max range, and current location
# - deliveries: list of deliveries with location, urgency, and time windows
# - charging_stations: list with location, cost structure, and slot availability
# - drivers: list with driver id and availability

from flask import Flask, request, jsonify
from ortools.constraint_solver import pywrapcp, routing_enums_pb2
import os
from graphhopper_matrix import build_distance_matrix
from optimizer import optimize_vehicle_routes

# Grasshopper API Key
GRASSHOPPER_API_KEY = os.environ.get("GRASSHOPPER_API_KEY")

app: Flask = Flask(__name__)


@app.route("/optimize-trip", methods=["POST"])
def optimize():
    data = request.get_json()
    if not data:
        return jsonify({"error": "Missing JSON payload"}), 400
    # Required fields
    required_fields = [
        "locations",
        "num_vehicles", # Number of vehicles in the fleet
        "depots", # Indexes of the depots in the distance matrix (with private charging stations)
        "vehicles", # List of vehicles with state of charge, max range, and current location
        "deliveries", # List of deliveries with location, urgency, and time windows
        "charging_stations", # List of charging stations with location, cost structure, and slot availability
        "drivers", # List of drivers with id and availability
    ]
    for field in required_fields:
        if field not in data:
            return jsonify({"error": f"Missing required field: {field}"}), 400

    distance_matrix = build_distance_matrix(data["locations"], "car")
    num_vehicles = data["num_vehicles"]
    depots = data["depots"]
    vehicles = data["vehicles"]
    deliveries = data["deliveries"]
    charging_stations = data["charging_stations"]
    drivers = data["drivers"]

    # Call the optimization logic from optimizer.py
    result = optimize_vehicle_routes(
        distance_matrix,
        num_vehicles,
        depots,
        vehicles,
        deliveries,
        charging_stations,
        drivers,
    )
    return jsonify(result)


if __name__ == "__main__":
    app.run(port=5001)
