FROM nodered/node-red
USER root:root
COPY --chown=node-red:node-red flows.json /data/
COPY --chown=node-red:node-red settings.js /data/
USER node-red:node-red
RUN npm install node-red-dashboard \
    node-red-contrib-opcua
