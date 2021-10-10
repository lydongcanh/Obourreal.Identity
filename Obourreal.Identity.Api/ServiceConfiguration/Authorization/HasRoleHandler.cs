using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Obourreal.Identity.Api.ServiceConfiguration.Authorization
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        public IConfiguration Configuration { get; }

        public HasRoleHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            var roleClaimType = Configuration["Auth0:RoleClaimType"];
            if (!context.User.HasClaim(c => c.Type == roleClaimType))
            {
                return Task.CompletedTask;
            }

            var role = context.User.FindFirst(c => c.Type == roleClaimType && string.Equals(c.Value.ToString(), requirement.Role.ToString(), StringComparison.CurrentCultureIgnoreCase));
            if (role != null)
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }
}