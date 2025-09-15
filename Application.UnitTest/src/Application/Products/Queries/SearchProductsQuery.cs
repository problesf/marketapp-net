
using MarketNet.Application.Products.Dto;
using MarketNet.Application.Products.Queries;
using MarketNet.Domain.Entities.Products;

namespace Application.UnitTest.src.Application.Products.Queries;

public class SearchProductsQueryHandlerTests
{
    private Mock<IProductRepository> _repo = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {

    }
    [SetUp]
    public void SetUp()
    {
        _repo = new Mock<IProductRepository>();
    }
    [Test]
    public async Task HandleOk()
    {
        var request = new SearchProductsQuery
        {
            Code = "C-01",
            Name = "Phone",
            Description = "Android",
            MinPrice = 100m,
            MaxPrice = 500m,
            MinStock = 1,
            MaxStock = 50,
            MinTaxRate = 0.10m,
            MaxTaxRate = 0.21m,
            Currency = "EUR",
            IsActive = true
        };

        var products = new List<Product>
        {
            new Product("C-01", "Phone X", "Smartphone básico", 299m, 10, 0.21m, "EUR", true,1)
        };


        ProductSearchCriteria? captured = null;

        _repo.Setup(r => r.SearchProductsAsync(It.IsAny<ProductSearchCriteria>()))
             .Callback<ProductSearchCriteria>(c => captured = c)
             .ReturnsAsync(products);

        var sut = new SearchProductsQueryHandler(_repo.Object, TestBootstrap.Mapper);

        var result = await sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result[0].Id.Should().Be(0);
        result[0].Code.Should().Be("C-01");
        result[0].Name.Should().Be("Phone X");
        result[0].Price.Should().Be(299m);
        result[0].Currency.Should().Be("EUR");
        result[0].IsActive.Should().BeTrue();

        captured.Should().NotBeNull();
        captured!.Code.Should().Be(request.Code);
        captured.Name.Should().Be(request.Name);
        captured.Description.Should().Be(request.Description);
        captured.MinPrice.Should().Be(request.MinPrice);
        captured.MaxPrice.Should().Be(request.MaxPrice);
        captured.MinStock.Should().Be(request.MinStock);
        captured.MaxStock.Should().Be(request.MaxStock);
        captured.MinTaxRate.Should().Be(request.MinTaxRate);
        captured.MaxTaxRate.Should().Be(request.MaxTaxRate);
        captured.Currency.Should().Be(request.Currency);
        captured.IsActive.Should().Be(request.IsActive);

        _repo.Verify(r => r.SearchProductsAsync(It.IsAny<ProductSearchCriteria>()), Times.Once);
    }

    [Test]
    public async Task HadleOkEmptyLst()
    {
        _repo.Setup(r => r.SearchProductsAsync(It.IsAny<ProductSearchCriteria>()))
             .ReturnsAsync(new List<Product>());

        var sut = new SearchProductsQueryHandler(_repo.Object, TestBootstrap.Mapper);

        var result = await sut.Handle(new SearchProductsQuery(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
