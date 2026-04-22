# Ballot API

ASP.NET Core 10 service that serves election and candidate data. Data is seeded in-memory at startup using [Bogus](https://github.com/bchavez/Bogus) — no database required.

## Project layout

```
Ballot/
├── Ballot.API/           # Controllers, Program.cs, Dockerfile
├── Ballot.Application/   # Query handlers, DTOs (CQRS)
├── Ballot.Domain/        # Election + Candidate entities, value objects
└── Ballot.Infrastructure/ # In-memory election store, Bogus seed
```

## Endpoints

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `GET` | `/api/ballots` | Bearer token (via nginx) | List all active elections. |
| `GET` | `/api/ballots/{electionId}` | Bearer token (via nginx) | Get a single election with full candidate details. |
| `GET` | `/health` | None | Health check. |

### List elections response

```json
[
  {
    "id": "...",
    "slug": "general-election-2025",
    "title": "General Election 2025",
    "description": "...",
    "status": "Ongoing",
    "opensAt": "2025-01-01T00:00:00Z",
    "closesAt": "2025-12-31T00:00:00Z",
    "candidates": [{ "id": "...", "name": "...", "party": "..." }]
  }
]
```

> Auth is enforced by nginx, not by this service directly. The `X-Voter-Id` header is injected by nginx after token validation.

## Configuration

| Key | Description |
|-----|-------------|
| `Frontend__Url` | Allowed CORS origin |
| `ASPNETCORE_URLS` | Bind address (default `http://+:8082` in ECS) |

No database or secrets required.

## Local development

```bash
cd Ballot
dotnet run --project Ballot.API
```

No docker-compose needed. Mock elections are generated automatically on startup.

Swagger UI: `http://localhost:8082/swagger`

## Docker

```bash
docker build -t ballot .
docker run -p 8082:8082 \
  -e ASPNETCORE_URLS=http://+:8082 \
  -e Frontend__Url=http://localhost:3000 \
  ballot
```
