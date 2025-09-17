using FluentValidation;
using MarketNet.Application.Categories.Dto;

namespace MarketNet.Application.Categories.Validators;

public sealed class CategoryChildDtoValidator : AbstractValidator<CategoryChildDto>
{
    public CategoryChildDtoValidator()
    {
        // Consideramos "nueva" si Id == 0 (ajusta si tu Id es nullable)
        When(c => c.Id == 0, () =>
        {
            RuleFor(c => c.Slug)
                .NotEmpty().WithMessage("Slug es obligatorio para categorías hijas nuevas.")
                .MaximumLength(100);

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description es obligatoria para categorías hijas nuevas.")
                .MaximumLength(1000);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name es obligatorio para categorías hijas nuevas.")
                .MaximumLength(100);
        });
    }
}
