using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Application.IntegrationTest;

[SetUpFixture]
public class TestContainerBootstrap
{
    private static DotNet.Testcontainers.Containers.IContainer _pg = null!;
    public static string ConnectionString { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
           .AddJsonFile("appsettings.Testing.json", optional: true, reloadOnChange: false)
           .AddEnvironmentVariables()
           .Build();

        var useTcPg = config.GetValue<bool>("UseTestcontainersPostgres");

        if (!useTcPg)
        {
            TestContext.Progress.WriteLine("[TC] Testcontainers PostgreSQL desactivado.");
            return;
        }

        var username = "postgres";
        var password = "postgres";
        var database = "testdb";

        _pg = new ContainerBuilder()
            .WithImage("postgres:16-alpine")
            .WithEnvironment("POSTGRES_USER", username)
            .WithEnvironment("POSTGRES_PASSWORD", password)
            .WithEnvironment("POSTGRES_DB", database)
            .WithCleanUp(true)
            .WithName($"tests-postgres-{Guid.NewGuid():N}")
            .WithPortBinding(0, 5432)
            .Build();

        await _pg.StartAsync();

        var host = "localhost";
        var mapped = _pg.GetMappedPublicPort(5432);
        ConnectionString = $"Host={host};Port={mapped};Database={database};Username={username};Password={password};Pooling=true";

        Environment.SetEnvironmentVariable("ConnectionStrings__Default", ConnectionString);
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        if (_pg is not null)
        {
            await _pg.StopAsync();
            await _pg.DisposeAsync();
        }
    }
}
