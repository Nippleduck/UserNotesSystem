using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserNotesSystem.Authentication.Constants;
using UserNotesSystem.Data.Identity;
using UserNotesSystem.Data.Identity.Models;

namespace UserNotesSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IdentityService service) : ControllerBase
    {
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
        {
            var authDetails = await service.AuthenticateAsync(request);

            return Ok(authDetails);
        }

        [HttpPost]
        [Authorize(Policies.AdminOnlyAccess)]
        public async Task<ActionResult<string>> Register([FromBody] RegisterUserRequest request)
        {
            var userId = await service.RegisterUserAsync(request);

            return CreatedAtAction(nameof(GetUsers), userId);
        }

        [HttpGet]
        [Authorize(Policies.AdminOnlyAccess)]
        public async Task<ActionResult<UserDetailsResponse[]>> GetUsers()
        {
            var users = await service.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Authorize(Policies.AdminOnlyAccess)]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await service.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
