using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.ViewModel.Categories;
using Infrastructure.Interfaces.Categories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ui.HandShort;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        #region Constructor

        private readonly ISubCategory _subCategory;
        private readonly ICategory _category;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public SubCategoryController(ISubCategory subCategory, ICategory category, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _subCategory = subCategory;
            _category = category;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string categoryId)
        {
            List<SubCategory> subCategories;
            //بازگشت به صفحه ایدی نداریم 
            if (!string.IsNullOrEmpty(categoryId))
            {
                subCategories = await _subCategory.GetAllSubCategoriesByCategoriesId(int.Parse(categoryId));
                //برای بازگشت با اینکه بدانیم در کدام دسته هستیم به ایدی ان دسته نیاز داریم
                HttpContext.Session.SetInt32("CategoryId", int.Parse(categoryId));
            }
            else
            {
                var CategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
                if (CategoryId == 0)
                {
                    return RedirectToAction("Index", "Category", new { Areas = "Admin" });
                }
                subCategories = await _subCategory.GetAllSubCategoriesByCategoriesId(CategoryId);
            }
            var models = _mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(subCategories);

            return View(models);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> CreateSubCategory()
        {
            var models = new SubCategoryViewModel();
            var Categories = await _category.GetCategory(Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId")));
            models.CategoryName = Categories.CategoryName;
            return View(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubCategory([Bind("SubCategoryName")] SubCategoryViewModel subCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            subCategoryViewModel.CategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            var subCategory = _mapper.Map<SubCategoryViewModel, SubCategory>(subCategoryViewModel);
            await _subCategory.AddSubCategory(subCategory);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> UpdateSubCategory(string id)
        {
            var subCategory = await _subCategory.GetSubCategory(int.Parse(id));
            var model = _mapper.Map<SubCategory, SubCategoryViewModel>(subCategory);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSubCategory([Bind("Id,SubCategoryName")] SubCategoryViewModel subCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(subCategoryViewModel);
            }
            subCategoryViewModel.CategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            var subCategory = _mapper.Map<SubCategoryViewModel, SubCategory>(subCategoryViewModel);
            var model = _subCategory.UpdateSubCategory(subCategory);
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return View(subCategoryViewModel);
            }
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> DeleteSubCategory(string id)
        {
            var model = await _subCategory.GetSubCategory(int.Parse(id));
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            _subCategory.DeleteSubCategory(model);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
