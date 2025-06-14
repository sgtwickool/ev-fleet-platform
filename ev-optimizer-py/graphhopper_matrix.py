# graphhopper_matrix.py
"""
Handles requests to the GraphHopper Matrix API and builds distance matrices for optimization.
"""
import os
import requests

# Remove the hardcoded API key and use environment variable instead
GRAPHOPPER_API_KEY = os.environ.get("GRASSHOPPER_API_KEY")
GRAPHOPPER_MATRIX_URL = f"https://graphhopper.com/api/1/matrix?key={GRAPHOPPER_API_KEY}"


def build_distance_matrix(locations, profile="car"):
    """
    Given a list of [lat, lon] pairs, returns the distance matrix using GraphHopper Matrix API.
    """
    if not GRAPHOPPER_API_KEY:
        raise ValueError(
            "Missing GraphHopper API key in environment variable GRASSHOPPER_API_KEY"
        )
    payload = {
        "points": locations,
        "profile": profile,
        "out_arrays": ["distances"],
        "fail_fast": False,
    }
    response = requests.post(GRAPHOPPER_MATRIX_URL, json=payload)
    if response.status_code != 200:
        raise RuntimeError(
            f"GraphHopper API error: {response.status_code} {response.text}"
        )
    data = response.json()
    return data["distances"]
