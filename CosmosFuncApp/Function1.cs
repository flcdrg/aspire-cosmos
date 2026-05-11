using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CosmosFuncApp;

public class Function1(ILogger<Function1> logger, CosmosClient cosmosClient)
{
    [Function("Function1")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var container = cosmosClient.GetContainer("db", "container");

        var response = await container.CreateItemAsync(new { id = Guid.NewGuid().ToString(), name = "Fred" });

        return new OkObjectResult($"Created item with id: {response.Resource.id}");
    }
}