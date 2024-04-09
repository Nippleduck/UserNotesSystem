using System.Security.Claims;
using UserNotesSystem.Core.Constants;

namespace UserNotesSystem.Server
{
    public class CurrentUserAccessor
    {
        public CurrentUserAccessor(IHttpContextAccessor contextAccessor)
        {
            UserId = contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User is not authorized");
        }

        public string UserId { get; }
    }
}
