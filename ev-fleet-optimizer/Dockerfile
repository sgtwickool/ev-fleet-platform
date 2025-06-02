# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EvFleetOptimizer.API/EvFleetOptimizer.API.csproj", "EvFleetOptimizer.API/"]
COPY ["EvFleetOptimizer.Core/EvFleetOptimizer.Core.csproj", "EvFleetOptimizer.Core/"]
COPY ["EvFleetOptimizer.Infrastructure/EvFleetOptimizer.Infrastructure.csproj", "EvFleetOptimizer.Infrastructure/"]
COPY ["EvFleetOptimizer.Services/EvFleetOptimizer.Services.csproj", "EvFleetOptimizer.Services/"]
RUN dotnet restore "EvFleetOptimizer.API/EvFleetOptimizer.API.csproj"
COPY . .
WORKDIR "/src/EvFleetOptimizer.API"
RUN dotnet build "EvFleetOptimizer.API.csproj" -c Release -o /app/build
RUN dotnet publish "EvFleetOptimizer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
# Install vsdbg for VS Code debugging
RUN apt-get update && apt-get install -y curl && \
    curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
ENTRYPOINT ["dotnet", "EvFleetOptimizer.API.dll"]
