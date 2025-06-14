"""
optimizer.py
Main entry point for EV fleet route optimization.
"""

import logging
from typing import Any, Dict, List, Optional
from ortools.constraint_solver import pywrapcp, routing_enums_pb2
from constraints import (
    add_distance_dimension,
    add_time_windows,
    restrict_unavailable_vehicles,
)
from extraction import extract_routes
from utils import validate_inputs, MAX_TIME, DEFAULT_TIME_WINDOW

# Configure logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class EVFleetOptimizer:
    """
    Optimizer for electric vehicle fleet routing with SoC and charging constraints.
    """

    def __init__(self, config: Optional[Dict[str, Any]] = None):
        self.config = config or {}

    def optimize_vehicle_routes(
        self,
        distance_matrix: List[List[int]],
        num_vehicles: int,
        depots: Any,
        vehicles: List[Dict[str, Any]],
        deliveries: List[Dict[str, Any]],
        charging_stations: List[Dict[str, Any]],
        drivers: Optional[List[Dict[str, Any]]] = None,
    ) -> Dict[str, Any]:
        """
        Optimize vehicle routes based on the provided distance matrix and advanced payload.
        Returns a dict with routes and status message.
        """
        try:
            validate_inputs(distance_matrix, num_vehicles, depots, vehicles, deliveries)
        except ValueError as e:
            logger.error(f"Input validation failed: {e}")
            return {"message": str(e)}

        # Handle depots: OR-Tools expects int for single depot, or two lists for multiple starts/ends
        if isinstance(depots, list) and len(depots) == 1:
            depots = depots[0]
        manager = pywrapcp.RoutingIndexManager(
            len(distance_matrix), num_vehicles, depots
        )
        routing = pywrapcp.RoutingModel(manager)

        # --- Constraints ---
        max_ranges = [
            v.get("soc", 100) / 100.0 * v.get("max_range", 200) for v in vehicles
        ]
        # Add distance dimension and charging constraints
        distance_dimension = add_distance_dimension(
            routing, manager, distance_matrix, max_ranges
        )
        if charging_stations:
            from constraints import add_charging_constraints

            add_charging_constraints(
                distance_dimension, manager, charging_stations, max_ranges
            )
        if any("time_window" in d for d in deliveries):
            add_time_windows(routing, manager, distance_matrix, deliveries, depots)
        if drivers:
            restrict_unavailable_vehicles(routing, drivers, num_vehicles)

        # --- Solve and Extract Solution ---
        search_parameters = pywrapcp.DefaultRoutingSearchParameters()
        search_parameters.first_solution_strategy = (
            routing_enums_pb2.FirstSolutionStrategy.PATH_CHEAPEST_ARC
        )
        search_parameters.local_search_metaheuristic = (
            routing_enums_pb2.LocalSearchMetaheuristic.GUIDED_LOCAL_SEARCH
        )
        search_parameters.time_limit.seconds = self.config.get("solver_time_limit", 10)

        logger.info("Starting solver...")
        solution = routing.SolveWithParameters(search_parameters)
        if solution:
            logger.info("Solution found.")
            charging_locations = (
                set(cs["location"] for cs in charging_stations)
                if charging_stations
                else set()
            )
            routes = extract_routes(
                solution,
                routing,
                manager,
                num_vehicles,
                max_ranges,
                charging_locations,
                distance_matrix,
            )
            # Human-readable formatting
            readable_routes = []
            for vehicle_idx, route in enumerate(routes):
                steps = []
                for step in route:
                    loc = step["location"]
                    soc = step["soc"]
                    is_cs = step["is_charging_station"]
                    cs_str = " (charging station)" if is_cs else ""
                    steps.append(f"Location {loc}{cs_str}, SoC: {soc:.1f}")
                readable_routes.append({"vehicle": vehicle_idx + 1, "route": steps})
            return {
                "routes": routes,
                "readable_routes": readable_routes,
                "message": "Optimization successful. See 'readable_routes' for details.",
            }
        else:
            logger.warning("No solution found.")
            return {"message": "No solution found."}


def optimize_vehicle_routes(*args, **kwargs):
    """
    Backward-compatible wrapper for the EVFleetOptimizer class.
    """
    optimizer = EVFleetOptimizer()
    return optimizer.optimize_vehicle_routes(*args, **kwargs)
