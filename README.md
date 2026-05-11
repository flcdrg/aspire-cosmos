# Aspire + Azure Cosmos DB Emulator Sample

This repository demonstrates a minimal [Aspire](https://aspire.dev/) distributed application that wires an Azure Functions app to an Azure Cosmos DB emulator.

The solution is useful as a starting point when you want to:

- develop locally against Cosmos DB without creating cloud resources
- pass Cosmos connection information into services through Aspire resource references
- test end-to-end writes from an HTTP-triggered function into a Cosmos container

## What the application does

The AppHost starts and orchestrates two resources:

- `cosmos`: an Azure Cosmos DB emulator (preview integration) with Data Explorer enabled
- `app`: an Azure Functions project (`CosmosFuncApp`)

At startup, the AppHost also declares:

- a database named `db`
- a container named `container` with partition key path `/name`

The function app receives Cosmos configuration via Aspire references, then exposes an HTTP endpoint that inserts a document into the container and returns the created item id.

## Request flow

1. You call the `Function1` HTTP-triggered function.
2. The function resolves a `CosmosClient` from dependency injection.
3. It writes a new item to database `db`, container `container`.
4. It responds with `Created item with id: <guid>`.

## Project structure

- `aspire-cosmos.AppHost/`: Aspire orchestration entry point
- `CosmosFuncApp/`: Azure Functions app that writes to Cosmos DB
- `aspire-cosmos.slnx`: solution file

## Running locally

Prerequisites:

- .NET SDK (matching the project target framework)
- Docker Desktop (required by the Cosmos emulator) or Podman
- [Aspire CLI](https://aspire.dev/get-started/install-cli/)

Run the distributed app with the Aspire CLI:

```bash
aspire run
```

Or with `dotnet`:

```bash
dotnet run --project aspire-cosmos.AppHost
```

Then call the function endpoint from the Aspire dashboard or with an HTTP client. Each request creates a new document in the emulator-backed container.

## Notes

- The Cosmos emulator integration uses preview APIs and may change.
- This sample intentionally keeps the function logic simple to focus on Aspire + Cosmos wiring.
