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
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "Roles",
                    RoleClaimType = configuration["Auth0:RoleClaimType"]
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement("read:users", domain)));
            });
            
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            return services;
        }
    }
}