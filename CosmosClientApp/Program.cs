using System.Net;
using Microsoft.Azure.Cosmos;

Console.WriteLine("Hello, World!");

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__cosmos")
    ?? throw new InvalidOperationException("Connection string 'ConnectionStrings__cosmos' was not provided.");

Console.WriteLine("Using cosmos account connection string from Aspire reference.");

using CosmosClient client = new(
    accountEndpoint: "https://localhost:8081/",
    authKeyOrResourceToken: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
);

Database database = await client.CreateDatabaseIfNotExistsAsync(
    id: "cosmicworks",
    throughput: 400
);