FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Ballot.Domain/Ballot.Domain.csproj Ballot.Domain/
COPY Ballot.Application/Ballot.Application.csproj Ballot.Application/
COPY Ballot.Infrastructure/Ballot.Infrastructure.csproj Ballot.Infrastructure/
COPY Ballot.API/Ballot.API.csproj Ballot.API/

RUN dotnet restore Ballot.API/Ballot.API.csproj

COPY . .

RUN dotnet publish Ballot.API/Ballot.API.csproj \
-c Release \
-o /app/publish \
--no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN groupadd --system --gid 1001 appgroup && \
    useradd --system --uid 1001 --gid appgroup appuser

RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

RUN chown -R appuser:appgroup /app

USER appuser

EXPOSE 8080

HEALTHCHECK --interval=10s --timeout=5s --start-period=30s --retries=3 \
  CMD curl -sf http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "Ballot.API.dll"]
