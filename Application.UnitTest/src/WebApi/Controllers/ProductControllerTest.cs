using MarketNet.src.Application.Products.Queries;
using MarketNet.src.WebApi.Controllers;
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
            var productCode = "PRODUCT-CODE";
            var name = "PRODUCT-NAME";
            var description = "Product example descripcion";
            var stock = 1;
            var taxRate = 1;
            var currency = "EUR";

            CreateProductCommand command = new CreateProductCommand
            {
                Code = productCode,
                Name = name,
                Description = description,
                Stock = stock,
                TaxRate = taxRate,
                Currency = currency
            };
            var results = actual.create(command);
            _mediatorMock.Verify(x => x.Send(It.Is<CreateProductCommand>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Update()
        {
            var actual = new ProductController(_mediatorMock.Object);
            var productCode = "PRODUCT-CODE";
            var name = "PRODUCT-NAME";
            var description = "Product example descripcion";
            var stock = 1;
            var taxRate = 1;
            var currency = "EUR";

            UpdateProductCommand command = new UpdateProductCommand
            {
                Code = productCode,
                Name = name,
                Description = description,
                Stock = stock,
                TaxRate = taxRate,
                Currency = currency
            };
            var results = actual.update(command);
            _mediatorMock.Verify(x => x.Send(It.Is<UpdateProductCommand>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByFilter()
        {
            var actual = new ProductController(_mediatorMock.Object);
            var productCode = "PRODUCT-CODE";
            var name = "PRODUCT-NAME";
            var description = "Product example descripcion";
            var stock = 1;
            var taxRate = 1;
            var price = 1;
            var currency = "EUR";

            SearchProductsQuery command = new SearchProductsQuery
            {
                Code = productCode,
                Name = name,
                Description = description,
                MinStock = stock,
                MaxStock = stock,
                MinTaxRate = taxRate,
                MaxTaxRate = taxRate,
                MinPrice = price,
                MaxPrice = price,
                Currency = currency
            };
            var results = actual.SearchByFilter(command);
            _mediatorMock.Verify(x => x.Send(It.Is<SearchProductsQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByCode()
        {
            var actual = new ProductController(_mediatorMock.Object);
            var productCode = "PRODUCT-CODE";

            SearchProductsByProductCodeOrIdQuery command = new SearchProductsByProductCodeOrIdQuery
            {
                Code = productCode,
            };
            var results = actual.Search(command);
            _mediatorMock.Verify(x => x.Send(It.Is<SearchProductsByProductCodeOrIdQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchById()
        {
            var actual = new ProductController(_mediatorMock.Object);
            var id = 0;

            SearchProductsByProductCodeOrIdQuery command = new SearchProductsByProductCodeOrIdQuery
            {
                Id = id,
            };
            var results = actual.Search(command);
            _mediatorMock.Verify(x => x.Send(It.Is<SearchProductsByProductCodeOrIdQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)), It.IsAny<CancellationToken>()), Times.Once());
        }
    }

}
