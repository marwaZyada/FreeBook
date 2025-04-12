using Domain.Constant;
using Domain.Entity;
using Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var user1 = new ApplicationUser()
                {
                    Email = "marwasharaf@gmail.com",
                    UserName = "marwaSharaf",
                    ImageUser = "/marwa.jpg",
                    Name = "superadmin",
                    ActiveUser = true
                };
                var user2 = new ApplicationUser()
                {
                    Email = "ahmedsharaf@gmail.com",
                    UserName = "ahmedSharaf",
                    ImageUser = "/ahmed.jpg",
                    Name = "admin",
                    ActiveUser = true
                };
                await userManager.CreateAsync(user1, "marwa");
                await userManager.AddToRoleAsync(user1, Helper.Roles.SuperAdmin.ToString());
                await userManager.CreateAsync(user2, "ahmed");
                await userManager.AddToRoleAsync(user2, Helper.Roles.Admin.ToString());
              
            }
            await roleManager.SeedClaimsAsync();
        }
        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole> rolemanager)
        {
            var adminrole =await rolemanager.FindByNameAsync(Helper.Roles.SuperAdmin.ToString());

            //add permission claims
            var modules=Enum.GetValues(typeof(Helper.PermissionModuleName));
            foreach (var item in modules)
                await rolemanager.AddPermissionClaim(adminrole, item.ToString());
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> rolemanager
          ,IdentityRole  role, string module)
        {
            var allclaims =await rolemanager.GetClaimsAsync(role);
            var allpermissions = Permission.GeneratePermissionsFromModules(module);
            foreach (var item in allpermissions)
            {
                if (!allclaims.Any(x=>x.Type=="Permission"&&x.Value==item))
              
                    await rolemanager.AddClaimAsync(role,new System.Security.Claims.Claim(Helper.Permission,
                        item));
                
            }

        }

    }
}
