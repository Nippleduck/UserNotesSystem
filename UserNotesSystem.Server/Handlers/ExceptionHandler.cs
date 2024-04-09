using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserNotesSystem.Data.Identity.Exceptions;

namespace UserNotesSystem.Server.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ExceptionHandler()
        {
            exceptionHandlers = new()
            {
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessEsxception },
                { typeof(AuthenticationFailureException), HandleAuthenticationFailureException},
                { typeof(RegistrationFailureException), HandleRegistrationFailureException},
            };
        }

        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> exceptionHandlers;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (exceptionHandlers.TryGetValue(exceptionType, out Func<HttpContext, Exception, Task>? handle))
            {
                await handle(httpContext, exception);
                return true;
            }

            return false;
        }

        private async Task HandleUnauthorizedAccessEsxception(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = ex.Message
            });
        }

        private async Task HandleAuthenticationFailureException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Authentication failed",
                Detail = ex.Message
            });
        }

        private async Task HandleRegistrationFailureException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode= StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Registration failed",
                Detail = ex.Message
            });
        }
    }
}
