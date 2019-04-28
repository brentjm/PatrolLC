#docker run --detach --name node-red --user=root \
docker run --detach --name node-red \
    --publish 1880:1880 \
    --mount "type=volume,source=node-red-data,target=/data" \
    --network=ape \
    node-red-custom
