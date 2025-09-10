using AutoMapper;
using MarketNet.src.Infraestructure.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UnitTest.src.Application;

[SetUpFixture]
public class TestBootstrap
{
    public static IServiceProvider Services { get; private set; } = default!;
    public static IMapper Mapper => Services.GetRequiredService<IMapper>();

    [OneTimeSetUp]
    public void RunOnce()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
        });

        Services = services.BuildServiceProvider();
    }

    [OneTimeTearDown]
    public void Shutdown()
    {
        (Services as IDisposable)?.Dispose();
    }
}
