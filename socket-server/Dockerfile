FROM microsoft/dotnet:latest

RUN apt-get update && apt-get install -y vim-tiny

RUN useradd -rm -d /home/dotnet -s /bin/bash -U -u 1000 dotnet\
    && mkdir /home/dotnet/socket_server
COPY socket-server.cs socket_server.csproj /home/dotnet/socket_server/
RUN chown -R dotnet:dotnet /home/dotnet/socket_server
USER dotnet

WORKDIR /home/dotnet/socket_server
RUN dotnet add package Newtonsoft.Json\
    && dotnet publish -c release -r linux-x64 socket_server.csproj

CMD ["/bin/bash", "-c", "sleep 1000"]
#CMD ["/home/dotnet/socket_server/bin/release/netcoreapp2.1/linux-x64/socket_server"]
