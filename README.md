# Node-Red
Setting up node-red in a Docker container.

## Overview
* *Dockerfile*:
  * pull in latest node-red image [Node-RED](https://hub.docker.com/r/nodered/node-red-docker/)
  * install user defined node-red packages

* *docker_run.sh*:
  * create named volume *node-red-data*
  * create named network *ape*
  * start the container
    * detached mode
    * named *node-red*
    * publish ports 1880
    * mount the named volume *node-red-data*
    * link the network *ape*
  * copy *settings.js* to the container

## Getting started
1. clone this repository
2. run the docker script
`$./docker_run.sh`

# Author

**Brent Maranzano**

# License

This project is licensed under the MIT License - see the LICENSE file for details
