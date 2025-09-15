namespace MarketNet.Application.Products.Validators
{

    using FluentValidation;
    using MarketNet.src.Application.Auth.Commands;

    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email es obligatorio.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password es obligatoria.");
        }
    }

}