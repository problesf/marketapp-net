namespace MarketNet.src.Application.Categories.Validators
{

    using FluentValidation;
    using MarketNet.src.Application.Categories.Commands;

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(p => p.Slug)
                .NotEmpty().WithMessage("Slug es obligatorio.");

            RuleFor(p => p.Description)
                .MaximumLength(1000).When(p => !string.IsNullOrEmpty(p.Description))
                .WithMessage("Description no puede exceder 1000 caracteres.")
                .When(p => !string.IsNullOrEmpty(p.Description));

            RuleFor(p => p.Name)
                .MaximumLength(100).When(p => !string.IsNullOrEmpty(p.Name))
                .WithMessage("Name no puede exceder 100 caracteres.")
                .When(p => !string.IsNullOrEmpty(p.Name));

        }
    }

}