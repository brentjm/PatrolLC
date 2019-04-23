docker run --detach --name node-red --user=root \
    --publish 1880:1880 \
    --mount "type=volume,source=node-red-data,target=/data" \
    node-red
