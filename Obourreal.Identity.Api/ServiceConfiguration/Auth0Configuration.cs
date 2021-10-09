using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Obourreal.Identity.Api.ServiceConfiguration.Authorization;
using Obourreal.Identity.Core.Domain.Models;

namespace Obourreal.Identity.Api.ServiceConfiguration
{
    public static class Auth0Configuration
    {
        public static IServiceCollection AddAuth0(this IServiceCollection services, IConfiguration configuration)
        {
            string domain = $"https://{configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = configuration["Auth0:Audience"];
                
                // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`.
                // Map it to a different claim by setting the NameClaimType below.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement("read:users", domain)));
                
                options.AddPolicy("admin", policy => policy.Requirements.Add(new HasRoleRequirement(Role.Admin)));
                options.AddPolicy("user", policy => policy.Requirements.Add(new HasRoleRequirement(Role.User)));
            });
            
            services.AddSingleton<IAuthorizationHandler, HasRoleHandler>();

            return services;
        }
    }
}