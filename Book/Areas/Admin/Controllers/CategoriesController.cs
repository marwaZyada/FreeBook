using Domain.Entity;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IservicesRepository<Category> _servicesCategory;

        public CategoriesController(IservicesRepository<Category> ServicesCategory)
        {
            _servicesCategory = ServicesCategory;
        }
        public IActionResult Index()
        {
            var Categories = _servicesCategory.GetAll();
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

    }
}
