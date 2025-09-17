using FluentAssertions;
using FluentValidation;
using MarketNet.Application.Products.Commands;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Entities.User;
using MarketNet.Domain.Exceptions.Products;
using NUnit.Framework;
using static Application.IntegrationTest.Testing;

namespace Application.IntegrationTest.Products.Commands
{
    public class CreateProductCommandTest
    {
        private User _seller;

        private Category _category;

        private Product _product;

        [SetUp]
        public new async Task TestSetUp()
        {
            await ResetState();

            _seller = new User
            {
                Email = "email@gmail.com",
                PasswordHash = "passwordHashed",
                SellerProfile = new SellerProfile("store name")
            };
            _product = new Product
            {
                Code = "PRO-001",
                Name = "Product Example",
                Currency = "EUR",
                Description = "Description",
                Price = 10,
                Stock = 10,
                TaxRate = 10
            };
            _category = new Category("CategoryExample", "CategoryDescription", "CA-001", null,null);

            await AddAsync<User>(_seller);
            await AddAsync<Category>(_category);
            await AddAsync<Product>(_product);


        }


        [Test]
        public async Task CreateOk()
        {
            CreateProductCommand command = new CreateProductCommand
            {
                Code = "PRO-002",
                Name = "Product Example 2",
                Currency = "EUR",
                Description = "Description",
                Price = 10,
                Stock = 10,
                TaxRate = 10,
                CategoriesId = [_category.Id.Value]
            };
            var itemId = await SendAsync(command);

            var productSaved = await FindByKeysAsync<Product>(
                new object[] { itemId },
                "Categories", "Seller"
            );

            var productCategories = productSaved.Categories;

            Assert.That(productSaved, Is.Not.Null, "El producto debería existir en la BD");
            Assert.That(productCategories.Count(), Is.EqualTo(1), "El producto debería tener una categoría");

            Assert.Multiple(() =>
            {
                Assert.That(productSaved!.Code, Is.EqualTo(command.Code));
                Assert.That(productSaved.Description, Is.EqualTo(command.Description));
                Assert.That(productSaved.Price, Is.EqualTo(command.Price));
                Assert.That(productSaved.Currency, Is.EqualTo(command.Currency));
                Assert.That(productSaved.Stock, Is.EqualTo(command.Stock));
                Assert.That(productSaved.TaxRate, Is.EqualTo(command.TaxRate));

            });
        }
        [Test]
        public async Task CreateKoValidationError()
        {
            CreateProductCommand command = new CreateProductCommand
            {
                Code = "PRO-003",
                Name = "Product Example",
                Currency = "EURO",
                Description = "",
                Price = 10,
                Stock = 10,
                TaxRate = 10,
                CategoriesId = [_category.Id.Value]
            };
            await FluentActions.Invoking(() =>
              SendAsync(command)).Should()
                  .ThrowAsync<ValidationException>()
                  .Where(e => e.Message.Contains("Currency debe tener exactamente 3 caracteres."));
        }

        [Test]
        public async Task CreateKOExistingProductCode()
        {
            CreateProductCommand command = new CreateProductCommand
            {
                Code = "PRO-001",
                Name = "Product Example 2",
                Currency = "EUR",
                Description = "Description",
                Price = 10,
                Stock = 10,
                TaxRate = 10,
                CategoriesId = [_category.Id.Value]
            };

            var ex = Assert.ThrowsAsync<ProductExistException>(async () =>
                await SendAsync(command)
            );

            Assert.That(ex.ErrorType, Is.EqualTo("product_exists"));
        }
    }

}
