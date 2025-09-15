using FluentValidation.TestHelper;
using MarketNet.Application.Auth.Commands;
using MarketNet.Application.Products.Validators;

namespace Application.UnitTest.src.Application.Auth.Validators
{
    public class RegisterCustomerCommandValidatorTest
    {
        private RegisterCustomerCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new RegisterCustomerCommandValidator();
        }

        [Test]
        public void ValidateFailEmailRequired()
        {
            var model = new RegisterCustomerCommand { Email = "", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email es obligatorio.");
        }
        [Test]
        public void ValidateFailEmailFormatIncorrect()
        {
            var model = new RegisterCustomerCommand { Email = "em.e", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email con formato incorrecto");
        }
        [Test]
        public void ValidateFailPasswordRequired()
        {
            var model = new RegisterCustomerCommand { Email = "test@test.com", Password = "" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password es obligatoria.");
        }

        [Test]
        public void ValidateOk()
        {
            var model = new RegisterCustomerCommand { Email = "test@test.com", Password = "secret" };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
