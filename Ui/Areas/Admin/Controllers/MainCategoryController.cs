using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.ViewModel.Categories;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;
using Ui.HandShort;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainCategoryController : Controller
    {

        #region Constructor

        private readonly IMainCategory _mainCategory;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public MainCategoryController(IMainCategory mainCategory, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _mainCategory = mainCategory;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mainCategories = await _mainCategory.GetAllMainCategories();
            var models = _mapper.Map<List<MainCategory>, List<MainCategoryViewModel>>(mainCategories);
            return View(models);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult CreateMainCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMainCategory([Bind("MainCategoryName")] MainCategoryViewModel mainCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(mainCategoryViewModel);
            }
            var mainCategory = _mapper.Map<MainCategoryViewModel, MainCategory>(mainCategoryViewModel);
            await _mainCategory.AddMainCategory(mainCategory);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> UpdateMainCategory(string id)
        {
            var mainCategory = await _mainCategory.GetMainCategory(int.Parse(id));
            var model = _mapper.Map<MainCategory, MainCategoryViewModel>(mainCategory);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMainCategory([Bind("Id,MainCategoryName")] MainCategoryViewModel mainCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = Extension.AlertError();
                return View(mainCategoryViewModel);
            }
            var mainCategory = _mapper.Map<MainCategoryViewModel, MainCategory>(mainCategoryViewModel);
            var model = _mainCategory.UpdateMainCategory(mainCategory);
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return View(mainCategoryViewModel);
            }
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> DeleteMainCategory(string id)
        {
            var model = await _mainCategory.GetMainCategory(int.Parse(id));
            if (model is null)
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("Index");
            }
            _mainCategory.DeleteMainCategory(model);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("Index");
        }

        #endregion

    }
}
