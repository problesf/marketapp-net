using MarketNet.Application.Products.Commands;
using MarketNet.Application.Products.Queries;
using MarketNet.WebApi.Controllers;
using MediatR;
using Newtonsoft.Json;

namespace Application.UnitTest.src.WebApi.Controllers
{
    public class ProductControllerTest
    {
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [SetUp]
        public void SetUp()
        {
            _mediatorMock.Reset();
        }

        [Test]
        public void Create()
        {
            var actual = new ProductController(_mediatorMock.Object);

            CreateProductCommand command = new CreateProductCommand
            {
                Code = "PRODUCT-CODE",
                Name = "PRODUCT-NAME",
                Description = "Product example descripcion",
                Stock = 1,
                TaxRate = 1,
                Currency = "EUR"
            };

            var results = actual.Create(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<CreateProductCommand>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Update()
        {
            var actual = new ProductController(_mediatorMock.Object);

            var id = 42L;
            UpdateProductCommand command = new UpdateProductCommand
            {
                Name = "PRODUCT-NAME",
                Description = "Product example descripcion",
                Stock = 1,
                TaxRate = 1,
                Currency = "EUR"
            };

            var results = actual.Update(id, command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<UpdateProductCommand>(c =>
                    c.Id == id
                ),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByFilter()
        {
            var actual = new ProductController(_mediatorMock.Object);

            SearchProductsQuery command = new SearchProductsQuery
            {
                Code = "PRODUCT-CODE",
                Name = "PRODUCT-NAME",
                Description = "Product example descripcion",
                MinStock = 1,
                MaxStock = 1,
                MinTaxRate = 1,
                MaxTaxRate = 1,
                MinPrice = 1,
                MaxPrice = 1,
                Currency = "EUR"
            };

            var results = actual.SearchByFilter(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<SearchProductsQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByCode()
        {
            var actual = new ProductController(_mediatorMock.Object);

            SearchProductsByProductCodeOrIdQuery command = new SearchProductsByProductCodeOrIdQuery
            {
                Code = "PRODUCT-CODE",
            };

            var results = actual.Search(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<SearchProductsByProductCodeOrIdQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchById()
        {
            var actual = new ProductController(_mediatorMock.Object);

            SearchProductsByProductCodeOrIdQuery command = new SearchProductsByProductCodeOrIdQuery
            {
                Id = 0,
            };

            var results = actual.Search(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<SearchProductsByProductCodeOrIdQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
