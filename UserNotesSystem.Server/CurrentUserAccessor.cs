using System.Security.Claims;

namespace UserNotesSystem.Server
{
    public class CurrentUserAccessor(IHttpContextAccessor contextAccessor)
    {
        public string UserId => GetCurrentUserId(contextAccessor);

        private static string GetCurrentUserId(IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User is not authorized");
        }
    }
}
