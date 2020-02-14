FROM nodered/node-red-docker
USER root:root
RUN mkdir -p /usr/src/hostdir
RUN chown node-red:node-red /usr/src/hostdir
USER node-red:node-red
#COPY --chown=node-red:node-red settings.js /usr/src/node-red/.
RUN npm install node-red-dashboard \
    node-red-contrib-opcua
