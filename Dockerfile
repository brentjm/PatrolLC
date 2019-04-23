FROM nodered/node-red-docker
USER node-red
RUN npm install node-red-dashboard \
    node-red-contrib-opcua \
    node-red-contrib-cip-ethernet-ip \
    node-red-contrib-postgres-variable \
    node-red-contrib-influxdb \
    node-red-node-twilio \
    node-red-node-email \
    node-red-contrib-machine-learning \
    node-red-contrib-spreadsheet-in \
    node-red-contrib-viseo-google-spreadsheet \
    node-red-node-serialport \
    node-red-contrib-gpio
