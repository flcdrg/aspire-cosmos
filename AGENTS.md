# AGENTS

## Purpose

This repository is a minimal .NET Aspire sample showing how to run an Azure Functions app against an Azure Cosmos DB emulator locally.

Start with [README.md](README.md) for the user-facing overview and local setup.

## Project Map

- [aspire-cosmos.AppHost/AppHost.cs](aspire-cosmos.AppHost/AppHost.cs): Distributed app orchestration, Cosmos emulator setup, database/container declarations, and resource wiring.
- [CosmosFuncApp/Program.cs](CosmosFuncApp/Program.cs): Azure Functions isolated-worker startup and Cosmos client registration.
- [CosmosFuncApp/Function1.cs](CosmosFuncApp/Function1.cs): HTTP-triggered function that writes an item to Cosmos DB.

## Common Commands

- Run full distributed app (preferred): `aspire run`
- Run via dotnet: `dotnet run --project aspire-cosmos.AppHost`
- Build solution: `dotnet build aspire-cosmos.slnx`
- Build function app only: `dotnet build CosmosFuncApp/CosmosFuncApp.csproj`

## Conventions

- Keep resource names in sync across AppHost and Functions wiring:
  - Cosmos DB resource: `cosmos`
  - Database: `db`
  - Container: `container`
- Preserve alignment between AppHost declarations and function usage:
  - AppHost creates database/container
  - Function resolves `CosmosClient` and writes to `db/container`
- Keep README accurate when run commands, prerequisites, or resource names change.

## Pitfalls

- Cosmos emulator integration in AppHost is preview-based; behavior and APIs may change.
- Local runs require Docker Desktop or Podman available before starting the AppHost.
- Partition key is defined as `/name` in AppHost; item shape and queries must remain compatible.

## Documentation Strategy

- Link to existing docs instead of duplicating them.
- Update [README.md](README.md) when architecture, commands, or prerequisites change.
