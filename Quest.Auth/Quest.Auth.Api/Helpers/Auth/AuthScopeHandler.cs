using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Quest.Auth.Api.Helpers.Auth
{
    public class AuthScopeHandler : AuthorizationHandler<AuthScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthScopeRequirement requirement)
        {
            // Fetch all permissions
            var permissions = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer).ToList();

            // Succeed if the permissions array contains the required permissions
            if (permissions.Any(s => s.Value == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}