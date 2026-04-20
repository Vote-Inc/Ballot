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

COPY --from=build /app/publish .

RUN chown -R appuser:appgroup /app

USER appuser

EXPOSE 8080

ENTRYPOINT ["dotnet", "Ballot.API.dll"]
