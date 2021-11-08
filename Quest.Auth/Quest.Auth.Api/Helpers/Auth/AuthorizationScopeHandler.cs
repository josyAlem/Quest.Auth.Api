using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Quest.Auth.Api
{
    public class AuthorizationScopeHandler : AuthorizationHandler<AuthorizationScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationScopeRequirement requirement)
        {
            // If user does not have the permissions claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;
           
            // Split the permissions string into an array
            var permissions = context.User.FindFirst(c => c.Type == "permissions" && c.Issuer == requirement.Issuer).Value.Split(' ');

            // Succeed if the permissions array contains the required permissions
            if (permissions.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}