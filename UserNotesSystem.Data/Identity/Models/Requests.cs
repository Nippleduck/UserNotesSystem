using UserNotesSystem.Core.Constants;

namespace UserNotesSystem.Data.Identity.Models
{
    public record AuthenticationRequest(string UserName, string Password);

    public record RegisterUserRequest(string UserName, string Password, Roles Role);
}
