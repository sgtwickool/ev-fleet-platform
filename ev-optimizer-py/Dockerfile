# syntax=docker/dockerfile:1
FROM python:3.11-slim

# Install system updates and security patches
RUN apt-get update && apt-get upgrade -y && apt-get clean && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Install dependencies
COPY requirements.txt ./
RUN pip install --no-cache-dir --upgrade pip && pip install --no-cache-dir -r requirements.txt

# Copy the rest of the app
COPY . .

# Expose the port (matches docker-compose)
EXPOSE 5001

# Create a non-root user and switch to it
RUN useradd -m appuser && chown -R appuser /app
USER appuser

# Set environment variable for Flask
ENV FLASK_APP=app.py

# Run the Flask app
CMD ["flask", "run", "--host=0.0.0.0", "--port=5001"]
