using FluentValidation.TestHelper;
using MarketNet.Application.Auth.Queries;
using MarketNet.Application.Products.Validators;

namespace Application.UnitTest.src.Application.Auth.Validators
{
    public class LoginQueryValidatorTest
    {
        private LoginQueryValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new LoginQueryValidator();
        }

        [Test]
        public void ValidateFailEmailRequired()
        {
            var model = new LoginQuery { Email = "", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email es obligatorio.");
        }
        [Test]
        public void ValidateFailEmailFormatIncorrect()
        {
            var model = new LoginQuery { Email = "em.e", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email con formato incorrecto");
        }
        [Test]
        public void ValidateFailPasswordRequired()
        {
            var model = new LoginQuery { Email = "test@test.com", Password = "" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password es obligatoria.");
        }

        [Test]
        public void ValidateOk()
        {
            var model = new LoginQuery { Email = "test@test.com", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
