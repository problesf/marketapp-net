namespace MarketNet.Application.Products.Validators
{

    using FluentValidation;
    using MarketNet.Application.Auth.Queries;

    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email es obligatorio.")
                .EmailAddress().WithMessage("Email con formato incorrecto");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password es obligatoria.");
        }
    }

}