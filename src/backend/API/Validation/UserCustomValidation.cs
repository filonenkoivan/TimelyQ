using API.Contracts.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Validation
{
    public class UserCustomValidation : AbstractValidator<RequestRegister>
    {
        public UserCustomValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).WithMessage("Name must contain at least 2 letters");
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(2).WithMessage("Surname must contain at least 2 letters");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Field must not be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Field must not be empty");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(7).WithMessage("Password length must be at least 7 characters");
            RuleFor(x => x.PasswordConfirm).NotEmpty().Matches(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
