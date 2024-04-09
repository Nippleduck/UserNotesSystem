using Microsoft.IdentityModel.Tokens;

namespace UserNotesSystem.Authentication.Models
{
    public class TokenOptions
    {
        public SigningCredentials SigningCredentials { get; set; } = default!;

        public Func<Task<string>> GenerateJti => () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
