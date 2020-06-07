using Microsoft.AspNetCore.Identity;
using System;

namespace CM.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole()
        {
        }

        public AppRole(string roleName) 
            : base(roleName)
        {
        }
    }
}
