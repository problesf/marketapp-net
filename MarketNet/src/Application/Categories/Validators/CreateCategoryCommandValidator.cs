// Validator del COMMAND
using FluentValidation;
using MarketNet.Application.Categories.Commands;

namespace MarketNet.Application.Categories.Validators;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.Slug)
            .NotEmpty().WithMessage("Slug es obligatorio.")
            .MaximumLength(100);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name es obligatorio.")
            .MaximumLength(100);

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description es obligatoria.")
            .MaximumLength(1000);

        RuleForEach(p => p.ChildCategories)
            .SetValidator(new CategoryChildDtoValidator());

        RuleFor(x => x.ChildCategories)
            .Must(list =>
            {
                var slugs = list
                    .Where(c => c.Id == 0) // hijas nuevas
                    .Select(c => (c.Slug ?? string.Empty).Trim().ToLowerInvariant());
                return slugs.Distinct().Count() == slugs.Count();
            })
            .WithMessage("No puede haber categorías hijas nuevas con Slug duplicado en el request.");
    }
}
