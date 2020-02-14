# PatrolLC Connection
This will build a Docker service that will create an OPC UA server that receives
an OPC UA tag and translates it into an JSON object that is sent through a
Berkely socket to a service that uses the JSON to execute a command on the 
Waters Patrol LC.

## Getting started
1. clone the repository
2. Run docker-compose
* docker-compose up -d

# Author

**Brent Maranzano**

# License

This project is licensed under the MIT License - see the LICENSE file for details
