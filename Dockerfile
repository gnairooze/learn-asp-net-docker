# https://www.youtube.com/watch?v=yQtgY4VG3kM
# stage 1: build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# restore
COPY ["src/MyWebApiApp/MyWebApiApp.csproj", "MyWebApiApp/"]
RUN dotnet restore "MyWebApiApp/MyWebApiApp.csproj"

# build
COPY ["src/MyWebApiApp", "MyWebApiApp/"]
RUN dotnet build "MyWebApiApp/MyWebApiApp.csproj" -c Release -o /app/build

# stage 2: publish stage
FROM build AS publish
RUN dotnet publish "MyWebApiApp/MyWebApiApp.csproj" -c Release -o /app/publish
RUN ls /app/publish

# stage 3: run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5052
EXPOSE 5052
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyWebApiApp.dll"]
