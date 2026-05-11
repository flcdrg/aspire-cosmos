var builder = DistributedApplication.CreateBuilder(args);

var cosmos = builder.AddAzureCosmosDB("cosmos")
    .RunAsEmulator(emulator =>
    {
        //emulator.WithImage("");
    });

var app = builder.AddProject<Projects.CosmosClientApp>("app")
    .WaitFor(cosmos)
    .WithReference(cosmos)
    ;

builder.Build().Run();
