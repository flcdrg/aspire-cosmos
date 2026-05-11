using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CosmosFuncApp;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var connectionString = Environment.GetEnvironmentVariable("container");

        using CosmosClient client = new(connectionString, new CosmosClientOptions()
        {
            LimitToEndpoint = true,
            ConnectionMode = ConnectionMode.Gateway
        });

        var container = client.GetContainer("db", "container");

        var response = await container.CreateItemAsync(new { id = Guid.NewGuid().ToString(), name = "Fred" });

        return new OkObjectResult($"Created item with id: {response.Resource.id}");
    }
}