using MarketNet.Application.Categories.Commands;
using MarketNet.Application.Categories.Queries;
using MarketNet.WebApi.Controllers;
using MediatR;
using Newtonsoft.Json;

namespace Application.UnitTest.src.WebApi.Controllers
{
    public class CategoriesControllerTest
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
            var actual = new CategoriesController(_mediatorMock.Object);

            CreateCategoryCommand command = new CreateCategoryCommand
            {
                Slug = "CA-CODE",
                Name = "CATEGORY-NAME",
                Description = "Category example descripcion",
            };

            var results = actual.Create(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<CreateCategoryCommand>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void Update()
        {
            var actual = new CategoriesController(_mediatorMock.Object);

            var id = 42L;
            UpdateCategoryCommand command = new UpdateCategoryCommand
            {
                Name = "CATEGORY-NAME",
                Description = "Category example descripcion",
            };

            var results = actual.Update(id, command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<UpdateCategoryCommand>(c =>
                    c.Id == id
                ),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByFilter()
        {
            var actual = new CategoriesController(_mediatorMock.Object);

            SearchCategoriesQuery command = new SearchCategoriesQuery
            {
                Name = "CATEGORY-NAME",
                Description = "Category example descripcion",

            };

            var results = actual.Search(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<SearchCategoriesQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByIdentifierId()
        {
            var actual = new CategoriesController(_mediatorMock.Object);

            SearchCategoryByIdOrSlugQuery command = new SearchCategoryByIdOrSlugQuery
            {
                Id = 1,
            };

            var results = actual.SearchByIdentifiers(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<SearchCategoryByIdOrSlugQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void SearchByIdentifierSlug()
        {
            var actual = new CategoriesController(_mediatorMock.Object);

            SearchCategoryByIdOrSlugQuery command = new SearchCategoryByIdOrSlugQuery
            {
                Slug = "CA-EXAMPLE",
            };

            var results = actual.SearchByIdentifiers(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<SearchCategoryByIdOrSlugQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

    }
}
