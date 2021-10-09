using System;
using Microsoft.AspNetCore.Authorization;
using Obourreal.Identity.Core.Domain.Models;

namespace Obourreal.Identity.Api.ServiceConfiguration.Authorization
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public Role Role { get; }

        public HasRoleRequirement(Role role)
        {
            Role = role;
        }
    }
}