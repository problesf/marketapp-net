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
    public class UpdateProductCommandTest
    {
        private User _seller;

        private Category _category1;

        private Category _category2;

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
            _category1 = new Category("CategoryExample1", "CategoryDescription", "CA-001", null,null);
            _category2 = new Category("CategoryExample2", "CategoryDescription", "CA-002", null,null);

            await AddAsync<User>(_seller);
            await AddAsync<Category>(_category1);
            await AddAsync<Category>(_category2);
            await AddAsync<Product>(_product);


        }


        [Test]
        public async Task UpdateOk()
        {
            UpdateProductCommand command = new UpdateProductCommand
            {
                Id = _product.Id,
                Name = "Product Example Modified",
                CategoriesId = [_category1.Id.Value, _category2.Id.Value]
            };
            var productSaved = await SendAsync(command);
            var productCategories = productSaved.Categories;

            Assert.That(productSaved, Is.Not.Null, "El producto debería existir en la BD");
            Assert.That(productCategories.Count(), Is.EqualTo(2), "El producto debería tener dos categorías");
            Assert.That(productSaved.Name, Is.EqualTo(command.Name));

        }
        [Test]
        public async Task UpdateKOValidationError()
        {
            UpdateProductCommand command = new UpdateProductCommand
            {
                Id = _product.Id,
                Name = "xxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                CategoriesId = [_category1.Id.Value, _category2.Id.Value]
            };
            await FluentActions.Invoking(() =>
              SendAsync(command)).Should()
                  .ThrowAsync<ValidationException>()
                  .Where(e => e.Message.Contains("Name no puede exceder 25 caracteres"));

        }


        [Test]
        public async Task UpdateKOProductNotExist()
        {
            UpdateProductCommand command = new UpdateProductCommand
            {
                Id = 0,
                Name = "xxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                CategoriesId = [_category1.Id.Value, _category2.Id.Value]
            };

            var ex = Assert.ThrowsAsync<ProductNotFoundException>(async () =>
                await SendAsync(command)
            );

            Assert.That(ex.ErrorType, Is.EqualTo("product_not_found"));
        }
    }

}
