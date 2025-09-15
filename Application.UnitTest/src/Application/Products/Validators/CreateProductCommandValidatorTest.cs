using FluentValidation.TestHelper;
using MarketNet.Application.Products.Commands;
using MarketNet.Application.Products.Validators;

namespace Application.UnitTest.src.Application.Products.Validators
{
    public class CreateProductCommandValidatorTest
    {
        private CreateProductCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateProductCommandValidator();
        }

        private static string Str(int len) => new string('x', len);

        [Test]
        public void ValidateFailCodeRequired()
        {
            var model = new CreateProductCommand { Code = "", Name = "Name", Currency = "EUR", Description = "Description", Price = 10, Stock = 10, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code)
                  .WithErrorMessage("Code es obligatorio.");
        }

        [Test]
        public void ValidateFailCodeLenght()
        {
            const string code = "ABCDEFGMEHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var model = new CreateProductCommand { Code = code, Name = "Name", Currency = "EUR", Description = "Description", Price = 10, Stock = 10, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Code)
                  .WithErrorMessage("Code no puede exceder 50 caracteres.");
        }

        [Test]
        public void ValidateFailStockNegative()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = "Description", Price = 10, Stock = -1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Stock)
                  .WithErrorMessage("Stock no puede ser negativo.");
        }

        [Test]
        public void ValidateFailCurrencyIsRequired()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = null, Description = "Description", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Currency)
                  .WithErrorMessage("Currency es obligatorio.");
        }

        [Test]
        public void ValidateFailInvalidCurency()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EURO", Description = "Description", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Currency)
                  .WithErrorMessage("Currency debe tener exactamente 3 caracteres.");
        }


        [Test]
        public void ValidateOkNameNull()
        {
            var model = new CreateProductCommand { Code = "code", Name = null, Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void ValidateOkNameEmpty()
        {
            var model = new CreateProductCommand { Code = "code", Name = "", Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void ValidateOkNameMax25()
        {
            var model = new CreateProductCommand { Code = "code", Name = Str(25), Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void ValidateFailNameTooLong()
        {
            var model = new CreateProductCommand { Code = "code", Name = Str(26), Currency = "EUR", Description = "Desc", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name no puede exceder 25 caracteres.");
        }

        [Test]
        public void ValidateOkDescriptionNull()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = null, Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public void ValidateOkDescriptionEmpty()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = "", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public void ValidateOkDescriptionMax1000()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = Str(1000), Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Test]
        public void ValidateFailDescriptionTooLong()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = Str(1001), Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage("Description no puede exceder 1000 caracteres.");
        }


        [Test]
        public void ValidateFailPriceNegative()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = "Desc", Price = -0.01m, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Price)
                  .WithErrorMessage("Price debe ser mayor o igual a 0.");
        }

        [Test]
        public void ValidateOkPriceZero()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = "Desc", Price = 0m, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void ValidateOkPriceTwoDecimals()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = "Desc", Price = 9.99m, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void ValidateOk()
        {
            var model = new CreateProductCommand { Code = "code", Name = "Name", Currency = "EUR", Description = "Description", Price = 10, Stock = 1, TaxRate = 10 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
