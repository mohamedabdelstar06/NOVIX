using Microsoft.AspNetCore.Identity;
using System;

namespace NOVIX.Identity.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
