using Domain.Constant;
using Domain.Entity;
using Domain.Entity.Identity;
using Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Domain.Constant.Permission.Accounts.View)]
    public class AccountsController : Controller
    {
        #region Declaration
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        #endregion

        #region Constructor
        public AccountsController(RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Methods

        [Authorize(Domain.Constant.Permission.Roles.View)]
        public IActionResult Roles()
        {
            var model = new RolesViewModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(r => r.Name).ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Domain.Constant.Permission.Roles.Create)]
        public async Task<IActionResult> Roles(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {

                var role = new IdentityRole
                {
                    Id = model.NewRole.RoleId,
                    Name = model.NewRole.RoleName
                };

                //create
                if (role.Id == null)
                {
                    role.Id = Guid.NewGuid().ToString();
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        SessionMsg("success", "تم الحفظ", "تم حفظ المستخدم بنجاح");
                        return RedirectToAction("Roles");
                    }
                    else
                        SessionMsg("error", "لم يتم الحفظ", "لم يتم حفظ المستخدم بنجاح");

                }
                //update
                else
                {
                    var RoleUpdate = await _roleManager.FindByIdAsync(role.Id);
                    RoleUpdate.Id = model.NewRole.RoleId!;
                    RoleUpdate.Name = model.NewRole.RoleName;
                    var result = await _roleManager.UpdateAsync(RoleUpdate);
                    if (result.Succeeded)
                    {
                        SessionMsg("success", "تم التعديل", "تم تعديل المستخدم بنجاح");

                        return RedirectToAction("Roles");
                    }
                    else
                        SessionMsg("error", "لم يتم التعديل", "لم يتم تعديل المستخدم بنجاح");

                }

            }
            return RedirectToAction("Roles");
        }



        //delete role
        [Authorize(Domain.Constant.Permission.Roles.Delete)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);


            await _roleManager.DeleteAsync(role);


            return RedirectToAction("Roles");
        }


        [HttpGet]
        [Authorize(Domain.Constant.Permission.Register.View)]
        public async Task<IActionResult> Register()
        {

            var model = new RegisterViewModel()
            {
                NewUser = new NewRegister(),
                Roles = _roleManager.Roles.OrderBy(r => r.Name).ToList(),
                Users = _userManager.Users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    ImageUser = u.ImageUser,
                    Email = u.Email,
                    ActiveUser = u.ActiveUser,
                    Role = string.Join(',', _userManager.GetRolesAsync(u).Result)
                }).OrderBy(u => u.Name).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Domain.Constant.Permission.Register.Create)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var ImageName = "";
            var file = HttpContext.Request.Form.Files;
            if ((file.Count() > 0))
            {

                ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                model.NewUser.ImageUser = ImageName;
            }
            else
            {
                model.NewUser.ImageUser = "";
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.NewUser.ImageUser))
                {
                    var path = Path.Combine(@"wwwroot", Domain.Entity.Helper.PathImageUser, ImageName);
                    if (!System.IO.File.Exists(path))
                    {
                        var filestream = new FileStream(path, FileMode.Create);
                        file[0].CopyTo(filestream);
                    }
                }






                var user = new ApplicationUser()
                {
                    Id = model.NewUser.Id!,
                    Name = model.NewUser.Name,
                    UserName = model.NewUser.Email.Split('@')[0],
                    Email = model.NewUser.Email,
                    ActiveUser = model.NewUser.ActiveUser,

                    ImageUser = model.NewUser.ImageUser,

                };

                if (user.Id == null)
                {
                    user.Id = Guid.NewGuid().ToString();
                    //create user
                    var result = await _userManager.CreateAsync(user, model.NewUser.Password);
                    if (result.Succeeded)
                    {
                        var role = await _userManager.AddToRoleAsync(user, model.NewUser.RoleName);
                        if (role.Succeeded)
                        {
                            SessionMsg("success", "تم الحفظ", "تم حفظ المستخدم بنجاح");


                            return RedirectToAction("Register");
                        }
                        else
                            SessionMsg("error", "لم يتم الحفظ", "لم يتم حفظ مجموعةالمستخدم بنجاح");

                    }
                    else

                        SessionMsg("error", "لم يتم الحفظ", "لم يتم حفظ المستخدم بنجاح");

                }

                //update user
                else
                {
                    var userupdate = await _userManager.FindByIdAsync(user.Id);
                    userupdate.Id = model.NewUser.Id;
                    userupdate.Name = model.NewUser.Name;
                    userupdate.UserName = model.NewUser.Email.Split('@')[0];
                    userupdate.Email = model.NewUser.Email;
                    userupdate.ActiveUser = model.NewUser.ActiveUser;
                    if (!string.IsNullOrEmpty(model.NewUser.ImageUser))
                        userupdate.ImageUser = model.NewUser.ImageUser;
                    var result = await _userManager.UpdateAsync(userupdate);
                    if (result.Succeeded)
                    {
                        var oldrole = await _userManager.GetRolesAsync(userupdate);
                        await _userManager.RemoveFromRolesAsync(userupdate, oldrole);
                        var addrole = await _userManager.AddToRoleAsync(userupdate, model.NewUser.RoleName);
                        if (addrole.Succeeded)
                            SessionMsg("success", "تم التعديل", "تم تعديل المستخدم بنجاح");

                        else
                            SessionMsg("error", "لم يتم التعديل", "لم يتم تعديل المستخدم بنجاح");

                    }
                    else
                        SessionMsg("error", "لم يتم التعديل", "لم يتم تعديل المستخدم بنجاح");


                }
                return RedirectToAction("Register");
            }

            return RedirectToAction("Register");
        }

        //delete user
        [Authorize(Domain.Constant.Permission.Register.Delete)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.ImageUser))
                {
                    var path = Path.Combine(@"wwwroot", Domain.Entity.Helper.DeleteImageUser, user.ImageUser);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(RegisterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.ChangePassword.UserId);
            if (user != null)
            {
                await _userManager.RemovePasswordAsync(user);
                var AddNewPass = await _userManager.AddPasswordAsync(user, model.ChangePassword.NewPassword);
                if (AddNewPass.Succeeded)
                    SessionMsg("success", " يتم التعديل", "تم تعديل كلمة المرور بنجاح");


                else
                    SessionMsg("error", "لم يتم التعديل", "لم يتم تعديل كلمة المرور بنجاح");

                return RedirectToAction("Register");
            }
            return RedirectToAction("Register");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)

                        return RedirectToAction("Index", "Home");
                    else
                        ViewBag.LoginError = false;

                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Accounts");
        }

        #endregion
        // msg
        private void SessionMsg(string msgtype, string title, string msg)
        {
            HttpContext.Session.SetString("msgtype", msgtype);
            HttpContext.Session.SetString("title", title);
            HttpContext.Session.SetString("msg", msg);
        }

    }
}