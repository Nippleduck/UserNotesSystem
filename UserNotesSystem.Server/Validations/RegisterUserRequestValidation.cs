using FluentValidation;
using UserNotesSystem.Data.Identity.Models;

namespace UserNotesSystem.Server.Validations
{
    public class RegisterUserRequestValidation : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidation() 
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}
