using FluentValidation;
using UserNotesSystem.Data.Identity.Models;

namespace UserNotesSystem.Server.Validations
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
