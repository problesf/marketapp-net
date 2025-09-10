using FluentValidation;
using MarketApp.src.Domain.entities.product;

namespace MarketNet.src.Application.Products.Validators
{

    using FluentValidation;
    using MarketNet.src.Application.Products.Criteria;
    using MarketNet.src.Application.Products.Dto;
    using MarketNet.src.Infraestructure.Repositories;
    using MediatR;

    public class CreateProductCommandValidator : AbstractValidator<ProductDto>
    {
        public CreateProductCommandValidator(IProductRepository? productRepo = null)
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code es obligatorio.")
                .MaximumLength(50).WithMessage("Code no puede exceder 50 caracteres.");

            When(_ => productRepo != null, () =>
            {
                RuleFor(p => p)
                    .MustAsync(async (p, ct) =>
                    {
                        ProductSearchCriteria criteria = new ProductSearchCriteria
                        {
                            Code = p.Code

                        };
                        List<Product> productList = (List<Product>)await productRepo.SearchProductsAsync(criteria);
                        return !productList.Any();

                    })
                    .WithMessage("Ya existe un producto con el mismo Code.");
            });

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock no puede ser negativo.");

            RuleFor(p => p.Description)
                .MaximumLength(1000).When(p => !string.IsNullOrEmpty(p.Description))
                .WithMessage("Description no puede exceder 1000 caracteres.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0m).WithMessage("Price debe ser mayor o igual a 0.")
                .Must(HasAtMostTwoDecimals).WithMessage("Price debe tener como máximo 2 decimales.");

            RuleFor(p => p.Currency)
                .NotEmpty().WithMessage("Currency es obligatorio.")
                .Length(3).WithMessage("Currency debe tener exactamente 3 caracteres.");

            RuleFor(p => p.TaxRate)
                .InclusiveBetween(0m, 100m).WithMessage("TaxRate debe estar entre 0 y 100.");

        }

        private static bool HasAtMostTwoDecimals(decimal value)
        {
            value = decimal.Round(value, 2);
            return (value * 100m) == decimal.Truncate(value * 100m);
        }
    }

}