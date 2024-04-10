using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserNotesSystem.Authentication;
using UserNotesSystem.Data.Identity.Exceptions;
using UserNotesSystem.Data.Identity.Models;

namespace UserNotesSystem.Data.Identity
{
    public class IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        TokenFactory tokenFactory)
    {
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await userManager.FindByNameAsync(request.UserName);
            if (user is null) throw new AuthenticationFailureException(
                $"User {request.UserName} does not exist");

            var signIn = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded) throw new AuthenticationFailureException(
                $"Wrong password for user {user.UserName}");

            var roles = await userManager.GetRolesAsync(user);
            var token = await tokenFactory.CreateTokenAsync(user.Id, roles[0]);

            var response = new AuthenticationResponse(user.Id, user.UserName!, roles[0], token);
            return response;
        }

        public async Task<string> RegisterUserAsync(RegisterUserRequest request)
        {
            var existing = await userManager.FindByNameAsync(request.UserName);
            if (existing is not null) throw new RegistrationFailureException(
                $"User {request.UserName} already exists");

            var user = new ApplicationUser { UserName = request.UserName };

            var creation = await userManager.CreateAsync(user, request.Password);
            if (!creation.Succeeded) throw new RegistrationFailureException(
                $"Failed to create user {user.UserName}");

            var roleAssignment = await userManager.AddToRoleAsync(user, request.Role.ToString());
            if (!roleAssignment.Succeeded) throw new RegistrationFailureException(
                $"Failed to add user {user.UserName} to the role {request.Role}");

            return user.Id;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user is not null) await userManager.DeleteAsync(user); 
        }

        public async Task<IEnumerable<UserDetailsResponse>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToArrayAsync();
            if (users.Length == 0) return [];  

            var usersDetails = new List<UserDetailsResponse>();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var detail = new UserDetailsResponse(user.Id, user.UserName!, roles[0]);
                usersDetails.Add(detail);
            }

            return usersDetails;
        }
    }
}
