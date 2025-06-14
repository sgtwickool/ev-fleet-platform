"""
utils.py
Utility functions and constants for EV fleet optimizer.
"""

import logging
from typing import List, Dict, Any


def setup_logging(level: int = logging.INFO):
    """
    Configures logging for the optimizer module.
    """
    logging.basicConfig(
        format="%(asctime)s %(levelname)s %(name)s: %(message)s",
        level=level,
    )


def validate_inputs(
    distance_matrix: List[List[int]],
    num_vehicles: int,
    depots: Any,
    vehicles: List[Dict[str, Any]],
    deliveries: List[Dict[str, Any]],
):
    """
    Validates input data for the optimizer.
    Raises ValueError if invalid.
    """
    if not distance_matrix or not vehicles or not deliveries:
        raise ValueError("Distance matrix, vehicles, and deliveries must be provided.")
    n = len(distance_matrix)
    for row in distance_matrix:
        if len(row) != n:
            raise ValueError("Distance matrix must be square.")
    if any(v.get("max_range", 0) <= 0 for v in vehicles):
        raise ValueError("All vehicles must have positive max_range.")
    if not isinstance(num_vehicles, int) or num_vehicles <= 0:
        raise ValueError("num_vehicles must be a positive integer.")
    if depots is None:
        raise ValueError("Depots must be provided.")


MAX_TIME = 10000
DEFAULT_TIME_WINDOW = [0, MAX_TIME]
