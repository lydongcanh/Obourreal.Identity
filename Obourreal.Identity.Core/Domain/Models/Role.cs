using System;
using System.ComponentModel;

namespace Obourreal.Identity.Core.Domain.Models
{
    [Flags]
    public enum Role
    {
        [Description("admin")]
        Admin = 1,
        [Description("user")]
        User = 2
    }
}