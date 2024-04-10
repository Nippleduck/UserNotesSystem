using Microsoft.IdentityModel.Tokens;

namespace UserNotesSystem.Authentication.Models
{
    public class TokenOptions
    {
        public SigningCredentials SigningCredentials { get; set; } = default!;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public Func<Task<string>> GenerateJti => () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
