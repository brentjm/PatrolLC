#!/bin/bash

################################################################################
# Script name: docker_run.sh
# Description: Builds a Docker container running Node-RED
# Args: None
# Author: Brent Maranzno
# email: brent_maranzano@gmail.com

# Usage:
# ./docker_run.sh
# see: https://hub.docker.com/r/nodered/node-red-docker/
################################################################################

echo "Bulding image"
docker build -t node-red-custom .

echo "Creating volume"
docker volume create node-red-data

echo "Creating network"
docker network create ape

echo "Starting container"
docker run --detach --name node-red \
    --publish 1880:1880 \
    --network=ape \
    --mount "type=volume,source=node-red-data,target=/data" \
    --mount "type=bind,source=c:/Users/FAST Lab/Documents/,target=/usr/src/test" \
    node-red-custom

echo "Complete"
