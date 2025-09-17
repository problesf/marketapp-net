using NUnit.Framework;
using static Application.IntegrationTest.Testing;

namespace Application.IntegrationTest
{
    public abstract class BaseTest
    {
        [SetUp]
        public async Task TestSetup() => await ResetState();
    }
}
