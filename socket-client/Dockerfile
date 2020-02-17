FROM microsoft/dotnet:latest

RUN mkdir /home/dotnet
COPY socket_client.cs /home/dotnet
COPY socket_client.csproj /home/dotnet

CMD ["/bin/bash", "-c", "sleep 1000"]
