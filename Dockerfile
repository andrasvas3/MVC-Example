FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Source

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o Publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build /Source/Publish .

ENV ASPNETCORE_ENVIRONMENT="Production"
ENV ASPNETCORE_HTTPS_PORTS=8080
ENV ASPNETCORE_URLS="http://+:8080"
ENV DOTNET_CONNECTION_STRING="Data Source=db.sqlite3"

ENTRYPOINT ["dotnet", "MVC-Example.dll"]
