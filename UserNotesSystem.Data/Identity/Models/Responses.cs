namespace UserNotesSystem.Data.Identity.Models
{
     public record AuthenticationResponse(string Id, string UserName, string Role, string Token);

     public record UserDetailsResponse(string Id, string UserName, string Role);
}
