using Microsoft.AspNetCore.Identity;
using System;

namespace NOVIX.Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
