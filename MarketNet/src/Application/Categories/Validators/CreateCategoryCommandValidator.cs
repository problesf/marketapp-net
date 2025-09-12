namespace MarketNet.Application.Categories.Validators
{

    using FluentValidation;
    using MarketNet.Application.Categories.Dto;

    public class CreateCategoryCommandValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Slug)
                .NotEmpty().WithMessage("Slug es obligatorio.")
                .MaximumLength(50).WithMessage("Code no puede exceder 50 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description es obligatorio.")
                .MaximumLength(1000).When(p => !string.IsNullOrEmpty(p.Description))
                .WithMessage("Description no puede exceder 1000 caracteres.");

            RuleFor(p => p.Name)
                  .NotEmpty().WithMessage("Name es obligatorio.")
                  .MaximumLength(100).When(p => !string.IsNullOrEmpty(p.Name))
                  .WithMessage("Name no puede exceder 100 caracteres.");

        }

        private static bool HasAtMostTwoDecimals(decimal value)
        {
            value = decimal.Round(value, 2);
            return (value * 100m) == decimal.Truncate(value * 100m);
        }
    }

}