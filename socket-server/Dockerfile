FROM microsoft/dotnet:latest

RUN useradd -rm -d /home/dotnet -s /bin/bash -U -u 1000 dotnet
USER dotnet
RUN mkdir /home/dotnet/socket_server
COPY socket-server.cs socket_server.csproj /home/dotnet/socket_server/

WORKDIR /home/dotnet/socket_server
RUN dotnet publish -c release -r linux-x64 socket_server.csproj

#CMD ["/bin/bash", "-c", "sleep 1000"]
CMD ["/home/dotnet/socket_server/bin/release/netcoreapp2.1/linux-x64/socket_server"]
