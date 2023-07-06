using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.ViewModel.Categories;
using Infrastructure.Interfaces.Categories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ui.HandShort;
using Core.ViewModel.Admin;
using Microsoft.AspNetCore.Authorization;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnImportantCategoryController : Controller
    {

        #region Constructor

        private readonly IUnImportantCategory _unImportantCategory;
        private readonly ISubCategory _subCategory;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public UnImportantCategoryController(IUnImportantCategory unImportantCategory, ISubCategory subCategory, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _unImportantCategory = unImportantCategory;
            _subCategory = subCategory;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string subCategoryId)
        {
            List<UnImportantCategory> unImportantCategories;
            //بازگشت به صفحه ایدی نداریم 
            if (!string.IsNullOrEmpty(subCategoryId))
            {
                unImportantCategories = await _unImportantCategory.GetAllUnImportantCategoriesBySubCategoriesId(int.Parse(subCategoryId));
                //برای بازگشت با اینکه بدانیم در کدام دسته هستیم به ایدی ان دسته نیاز داریم
                HttpContext.Session.SetInt32("SubCategoryId", int.Parse(subCategoryId));
            }
            else
            {
                var SubCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
                if (SubCategoryId == 0)
                {
                    return RedirectToAction("Index", "SubCategory", new { Areas = "Admin" });
                }
                unImportantCategories = await _unImportantCategory.GetAllUnImportantCategoriesBySubCategoriesId(SubCategoryId);
            }
            var models = _mapper.Map<List<UnImportantCategory>, List<UnImportantCategoryViewModel>>(unImportantCategories);

            return View(models);
        }

        #endregion

        #region Create

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateUnImportantCategory()
        {
            var models = new UnImportantCategoryViewModel();
            var Categories = await _subCategory.GetSubCategory(Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId")));
            models.SubCategoryName = Categories.SubCategoryName;
            return View(models);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUnImportantCategory([Bind("UnImportantCategoryName")] UnImportantCategoryViewModel unImportantCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            unImportantCategoryViewModel.SubCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
            var unImportantCategory = _mapper.Map<UnImportantCategoryViewModel, UnImportantCategory>(unImportantCategoryViewModel);
            await _unImportantCategory.AddUnImportantCategory(unImportantCategory);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateUnImportantCategory(string id)
        {
            var unImportantCategory = await _unImportantCategory.GetUnImportantCategory(int.Parse(id));
            var model = _mapper.Map<UnImportantCategory, UnImportantCategoryViewModel>(unImportantCategory);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUnImportantCategory([Bind("Id,UnImportantCategoryName")] UnImportantCategoryViewModel unImportantCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(unImportantCategoryViewModel);
            }
            unImportantCategoryViewModel.SubCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
            var unImportantCategory = _mapper.Map<UnImportantCategoryViewModel, UnImportantCategory>(unImportantCategoryViewModel);
            var model = _unImportantCategory.UpdateUnImportantCategory(unImportantCategory);
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return View(unImportantCategoryViewModel);
            }
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteUnImportantCategory(string id)
        {
            var model = await _unImportantCategory.GetUnImportantCategory(int.Parse(id));
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            _unImportantCategory.DeleteUnImportantCategory(model);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
