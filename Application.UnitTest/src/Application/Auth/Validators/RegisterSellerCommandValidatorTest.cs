using FluentValidation.TestHelper;
using MarketNet.Application.Auth.Commands;
using MarketNet.Application.Products.Validators;

namespace Application.UnitTest.src.Application.Auth.Validators
{
    public class RegisterSellerCommandValidatorTest
    {
        private RegisterSellerCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new RegisterSellerCommandValidator();
        }

        [Test]
        public void ValidateFailEmailRequired()
        {
            var model = new RegisterSellerCommand { Email = "", Password = "secret", StoreName = "name" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email es obligatorio.");
        }
        [Test]
        public void ValidateFailEmailFormatIncorrect()
        {
            var model = new RegisterSellerCommand { Email = "em.e", Password = "secret", StoreName = "name" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email con formato incorrecto");
        }
        [Test]
        public void ValidateFailPasswordRequired()
        {
            var model = new RegisterSellerCommand { Email = "test@test.com", Password = "", StoreName = "name" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password es obligatoria.");
        }
        [Test]
        public void ValidateFailStoreNameRequired()
        {
            var model = new RegisterSellerCommand { Email = "test@test.com", Password = "", StoreName = "name" };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password es obligatoria.");
        }
        [Test]
        public void ValidateOk()
        {
            var model = new RegisterSellerCommand { Email = "test@test.com", Password = "secret", StoreName = "name" };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
