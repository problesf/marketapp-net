using MarketNet.Application.Auth.Commands;
using MarketNet.Application.Auth.Queries;
using MediatR;
using Newtonsoft.Json;

namespace Application.UnitTest.src.WebApi.Controllers
{
    public class AuthControllerTest
    {
        private Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        [SetUp]
        public void SetUp()
        {
            _mediatorMock.Reset();
        }

        [Test]
        public void Login()
        {
            var actual = new AuthController(_mediatorMock.Object);

            LoginQuery query = new LoginQuery
            {
                Email = "email@email.com",
                Password = "EUR"
            };

            var results = actual.Login(query);

            _mediatorMock.Verify(x => x.Send(
                It.Is<LoginQuery>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(query)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void RegisterCustomer()
        {
            var actual = new AuthController(_mediatorMock.Object);

            RegisterCustomerCommand command = new RegisterCustomerCommand
            {
                Email = "email@email.com",
                Password = "EUR"
            };

            var results = actual.RegisterCustomer(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<RegisterCustomerCommand>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public void RegisterSeller()
        {
            var actual = new AuthController(_mediatorMock.Object);

            RegisterSellerCommand command = new RegisterSellerCommand
            {
                Email = "email@email.com",
                Password = "EUR",
                StoreName = "StoreName"
            };

            var results = actual.RegisterSeller(command);

            _mediatorMock.Verify(x => x.Send(
                It.Is<RegisterSellerCommand>(c => JsonConvert.SerializeObject(c) == JsonConvert.SerializeObject(command)),
                It.IsAny<CancellationToken>()), Times.Once());
        }

    }
}
