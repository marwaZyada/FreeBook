using Domain.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Domain.Constant.Permission.Home.View)]
    public class HomeController : Controller
    {
        [Authorize(Domain.Constant.Permission.Home.View)]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
