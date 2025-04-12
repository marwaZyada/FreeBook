using Domain.Constant;
using Domain.Entity;
using Domain.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;

        public PermissionsController(RoleManager<IdentityRole> rolemanager)
        {
            _rolemanager = rolemanager;
        }
        public async Task<IActionResult> Permissions(string roleid)
        {
            var role = await _rolemanager.FindByIdAsync(roleid);
              
            var claims = _rolemanager.GetClaimsAsync(role).Result.Select(x => x.Value);
            var allpemissions = Permission.PermissionsList()
            .Select(per => new RoleClaimsViewModel
            {
               
                Value = per
            }).ToList();
            foreach (var item in allpemissions)
            {
                if (claims.Any(x => x == item.Value))

                    item.IsSelected = true;
               
            }
            var pemissions = new PermissionViewModel()
            {
                RoleId = roleid,
                RoleName = role.Name,
                RoleClaims = allpemissions

            };
            return View(pemissions);
        }
        [HttpPost("UpdatePermission")]
    
        public async Task<IActionResult> UpdatePermissions(PermissionViewModel model)
        {
            var role=await _rolemanager.FindByIdAsync(model.RoleId);
            var claims=await _rolemanager.GetClaimsAsync(role);
            foreach (var item in claims)
               await _rolemanager.RemoveClaimAsync(role, item);
            
            var selectedclaims = model.RoleClaims.Where(x=>x.IsSelected==true).ToList();
            foreach (var item in selectedclaims)
             await   _rolemanager.AddClaimAsync(role, new Claim(Helper.Permission, item.Value));
                
           

            return RedirectToAction("Roles","Accounts");
        }
        }
}
