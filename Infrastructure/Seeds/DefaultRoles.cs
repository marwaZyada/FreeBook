using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager) {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = Helper.Roles.SuperAdmin.ToString()
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = Helper.Roles.Admin.ToString()
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = Helper.Roles.Basic.ToString()
                });
            }
           
        }
    }
}
