namespace MarketNet.Application.Products.Validators
{

    using FluentValidation;
    using MarketNet.Application.Products.Commands;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("Code es obligatorio.")
                .MaximumLength(50).WithMessage("Code no puede exceder 50 caracteres.");

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