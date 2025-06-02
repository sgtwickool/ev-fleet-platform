# EV Fleet Platform Monorepo

This monorepo contains:
- `ev-fleet-optimizer`: .NET Core API for EV fleet management
- `ev-optimizer-py`: Python API for optimization logic

## Structure
- `/ev-fleet-optimizer` - .NET Core solution and services
- `/ev-optimizer-py` - Python Flask API and optimizer
- `/docker-compose.yml` - Compose file for running services together

## Getting Started
1. Build and run with Docker Compose:
   ```bash
   docker-compose up --build
   ```
2. Access the .NET API at `http://localhost:5000` (or configured port)
3. Access the Python API at `http://localhost:5001`

## Development
- Each subproject maintains its own dependencies and documentation.
- Use the root `docker-compose.yml` to orchestrate both APIs and shared services (e.g., database).
