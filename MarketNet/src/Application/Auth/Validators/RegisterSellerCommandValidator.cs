namespace MarketNet.Application.Products.Validators
{

    using FluentValidation;
    using MarketNet.Application.Auth.Commands;

    public class RegisterSellerCommandValidator : AbstractValidator<RegisterSellerCommand>
    {
        public RegisterSellerCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email es obligatorio.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password es obligatoria.");
            RuleFor(p => p.StoreName)
                .NotEmpty().WithMessage("El nombre de la tienda es obligatoria.")
                .MaximumLength(30).WithMessage("El nombre de la tienda no puede tener mas de 30 caracteres");
        }
    }

}