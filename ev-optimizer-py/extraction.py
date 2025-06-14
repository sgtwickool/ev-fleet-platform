"""
extraction.py
Solution extraction helpers for EV fleet optimizer.
"""

import logging
from typing import List, Dict, Any
from ortools.constraint_solver import pywrapcp


def extract_routes(
    solution: pywrapcp.Assignment,
    routing: pywrapcp.RoutingModel,
    manager: pywrapcp.RoutingIndexManager,
    num_vehicles: int,
    max_ranges: List[float],
    charging_locations: set,
    distance_matrix: List[List[int]],
) -> List[List[Dict[str, Any]]]:
    """
    Extracts vehicle routes from the OR-Tools solution.
    """
    routes = []
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
                soc = max_ranges[vehicle_id]
            index = next_index
        route.append(
            {
                "location": manager.IndexToNode(index),
                "soc": soc,
                "is_charging_station": False,
            }
        )
        routes.append(route)
    return routes
