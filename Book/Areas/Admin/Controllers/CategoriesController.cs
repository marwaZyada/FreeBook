using AutoMapper;
using Domain.Entity;
using Domain.Entity.Identity;
using Domain.ViewModel;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IservicesRepository<Category> _servicesCategory;
        private readonly IServicesRepositoryLog<LogCategory> _servicesCategoryLog;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CategoriesController(IservicesRepository<Category> ServicesCategory,
            IServicesRepositoryLog<LogCategory> servicesCategoryLog,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _servicesCategory = ServicesCategory;
            _servicesCategoryLog = servicesCategoryLog;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Categories()
        {
            var Categories = new CategoryViewModel()
            {
                Categories = _servicesCategory.GetAll(),
                LogCategories = _servicesCategoryLog.GetAll(),
                NewCategory = new CategoryView()
            };

            return View(Categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userid = (_userManager.GetUserId(User));

                //save
                if (model.NewCategory.Id == null)
                {
                    if (_servicesCategory.GetBy(model.NewCategory.Name) != null)
                    {
                        SessionMsg("error", Resources.ResourceWeb.lbNotSave,
                            Resources.ResourceWeb.lbMsgDublicateNameCategory);
                    }
                    else
                    {
                        var category = _servicesCategory.Save(_mapper.Map<Category>(model.NewCategory));
                        if (_servicesCategoryLog.Save(category.Id, userid))
                        {
                            SessionMsg("success", Resources.ResourceWeb.lbSave,
                                                       Resources.ResourceWeb.lbMsgSave);
                        }
                        else
                        {
                            SessionMsg("error", Resources.ResourceWeb.lbNotSave,
                                                       Resources.ResourceWeb.lbMsgNotSave);
                        }
                    }

                }
                //update
                else
                {
                    var category = _servicesCategory.Save(_mapper.Map<Category>(model.NewCategory));
                    if (_servicesCategoryLog.Update(category.Id, userid))
                    {
                        SessionMsg("success", Resources.ResourceWeb.lbUpdate,
                                                       Resources.ResourceWeb.lbMsgUpdate);
                    }
                    else
                    {
                        SessionMsg("error", Resources.ResourceWeb.lbNotUpdate,
                                                       Resources.ResourceWeb.lbMsgNotUpdate);
                    }
                }
                return RedirectToAction(nameof(Categories));

            }
            return RedirectToAction(nameof(Categories));
        }

        public IActionResult Delete(Guid id)
        {
            var userid = (_userManager.GetUserId(User));
            if (_servicesCategory.Delete(id) && _servicesCategoryLog.Delete(id, userid))
                return RedirectToAction(nameof(Categories));
            return RedirectToAction(nameof(Categories));
        }

        public IActionResult DeleteLog(Guid id)
        {

            if (_servicesCategoryLog.DeleteLog(id))
                return RedirectToAction(nameof(Categories));


            return RedirectToAction(nameof(Categories));
        }
        private void SessionMsg(string msgtype, string title, string msg)
        {
            HttpContext.Session.SetString("msgtype", msgtype);
            HttpContext.Session.SetString("title", title);
            HttpContext.Session.SetString("msg", msg);
        }

    }
}
