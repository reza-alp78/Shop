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
    public class CategoryController : Controller
    {

        #region Constructor

        private readonly ICategory _category;
        private readonly IMainCategory _mainCategory;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public CategoryController(ICategory category, IMainCategory mainCategory, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _category = category;
            _mainCategory = mainCategory;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string mainCategoryId)
        {
            List<Category> categories;
            //بازگشت به صفحه ایدی نداریم 
            if (!string.IsNullOrEmpty(mainCategoryId))
            {
                categories = await _category.GetAllCategoriesByMainCategoriesId(int.Parse(mainCategoryId));
                //برای بازگشت با اینکه بدانیم در کدام دسته هستیم به ایدی ان دسته نیاز داریم
                HttpContext.Session.SetInt32("MainCategoryId",int.Parse(mainCategoryId));
            }
            else
            {             
                var MainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                if (MainCategoryId == 0)
                {
                    return RedirectToAction("Index", "MainCategory", new { Areas = "Admin" });
                }
                categories = await _category.GetAllCategoriesByMainCategoriesId(MainCategoryId);
            }
            var models = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);

            return View(models);
        }

        #endregion

        #region Create

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            var models = new CategoryViewModel();
            var mainCategories = await _mainCategory.GetMainCategory(Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId")));
            models.MainCategoryName = mainCategories.MainCategoryName;
            return View(models);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("CategoryName")] CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            categoryViewModel.MainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            var category = _mapper.Map<CategoryViewModel, Category>(categoryViewModel);
            await _category.AddCategory(category);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var category = await _category.GetCategory(int.Parse(id));
            var model = _mapper.Map<Category, CategoryViewModel>(category);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory([Bind("Id,CategoryName")] CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(categoryViewModel);
            }
            categoryViewModel.MainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            var category = _mapper.Map<CategoryViewModel, Category>(categoryViewModel);
            var model = _category.UpdateCategory(category);
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return View(categoryViewModel);
            }
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var model = await _category.GetCategory(int.Parse(id));
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            _category.DeleteCategory(model);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
