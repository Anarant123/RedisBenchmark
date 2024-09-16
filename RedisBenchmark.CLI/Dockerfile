FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RedisBenchmark.CLI/RedisBenchmark.CLI.csproj", "RedisBenchmark.CLI/"]
RUN dotnet restore "RedisBenchmark.CLI/RedisBenchmark.CLI.csproj"
COPY . .
WORKDIR "/src/RedisBenchmark.CLI"
RUN dotnet build "RedisBenchmark.CLI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RedisBenchmark.CLI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisBenchmark.CLI.dll"]
