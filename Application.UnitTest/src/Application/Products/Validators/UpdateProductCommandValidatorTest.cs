using FluentValidation.TestHelper;
using MarketNet.Application.Products.Commands;
using MarketNet.Application.Products.Validators;

namespace Application.UnitTest.src.Application.Products.Validators
{
    public class UpdateProductCommandValidatorTest
    {
        private UpdateProductCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateProductCommandValidator();
        }

        private static string Str(int len) => new string('x', len);


        [Test]
        public void ValidateFailStockNegative()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = "Description", Price = 10, Stock = -1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Stock)
                  .WithErrorMessage("Stock no puede ser negativo.");
        }

        [Test]
        public void ValidateFailInvalidCurency()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EURO", Description = "Description", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Currency)
                  .WithErrorMessage("Currency debe tener exactamente 3 caracteres.");
        }


        [Test]
        public void ValidateOkNameNull()
        {
            var model = new UpdateProductCommand { Name = null, Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void ValidateOkNameMax25()
        {
            var model = new UpdateProductCommand { Name = Str(25), Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void ValidateFailNameTooLong()
        {
            var model = new UpdateProductCommand { Name = Str(26), Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name no puede exceder 25 caracteres.");
        }

        [Test]
        public void ValidateOkDescriptionNull()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = null, Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public void ValidateOkDescriptionMax1000()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = Str(1000), Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public void ValidateFailDescriptionTooLong()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = Str(1001), Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage("Description no puede exceder 1000 caracteres.");
        }


        [Test]
        public void ValidateFailPriceNegative()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = "Desc", Price = -0.01m, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price)
                  .WithErrorMessage("Price debe ser mayor o igual a 0.");
        }

        [Test]
        public void ValidateOkPriceZero()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = "Desc", Price = 0m, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void ValidateOkPriceTwoDecimals()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = "Desc", Price = 9.99m, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void ValidateOk()
        {
            var model = new UpdateProductCommand { Name = "Name", Currency = "EUR", Description = "Description", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
