using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.Products;
using Core.ViewModel.Categories;
using Core.ViewModel.CategoriesAndProducts;
using Core.ViewModel.Products;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Categories;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Infrastructure.Interfaces.Products;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ui.HandShort;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductPeopertyController : Controller
    {

        #region Constructor

        private readonly IProductProperty _productProperty;
        private readonly IWhichCategory _whichCategory;
        private readonly ICategoryProductProperty _categoryProductProperty;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public ProductPeopertyController(IProductProperty productProperty, IWhichCategory whichCategory, ICategoryProductProperty categoryProductProperty, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _productProperty = productProperty;
            _whichCategory = whichCategory;
            _categoryProductProperty = categoryProductProperty;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Selected Is Update or Create

        [HttpGet]
        public async Task<IActionResult> ChoiesPropertyUpdateOrAddUnImportantCategory(string unImportantCategoryId)
        {
            int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            int subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
            //if be null means not exist and go to create productproperty
            var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId, int.Parse(unImportantCategoryId));
            if (whichCategory is null)
            {
                HttpContext.Session.SetInt32("UnImportantCategoryId", int.Parse(unImportantCategoryId));
                return RedirectToAction("ChoiesPropertyUnImportantCategory");
            }
            //if not null mean exist and go to update
            return RedirectToAction("UpdateChoiesPropertyUnImportantCategory");
        }

        [HttpGet]
        public async Task<IActionResult> ChoiesPropertyUpdateOrAddSubCategory(string subCategoryId)
        {
            int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            //if be null means not exist and go to create productproperty
            var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, int.Parse(subCategoryId));
            HttpContext.Session.SetInt32("WhichCategory_SubCategoryId", int.Parse(subCategoryId));
            HttpContext.Session.SetInt32("UnImportantCategoryId", 0);
            if (whichCategory is null)
            {              
                return RedirectToAction("ChoiesPropertySubCategory");
            }
            //if not null mean exist and go to update
            return RedirectToAction("UpdateChoiesPropertySubCategory");
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> UpdateChoiesPropertyUnImportantCategory()
        {
            int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            int subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
            int unImportantCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("UnImportantCategoryId"));

            var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId, unImportantCategoryId);
            var categoryProductProperty = await _categoryProductProperty.GetCategoryProductPropertyByWhichCategoryId(whichCategory.Id);
            var productProperty = await _productProperty.GetProductPropertyByProductPropertyId(categoryProductProperty.ProductPropertyId);

            var model = _mapper.Map<ProductProperty, ProductPropertyViewModel>(productProperty);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateChoiesPropertyUnImportantCategory(ProductPropertyViewModel productPropertyViewModel)
        {
            try
            {
                var productProperty = _mapper.Map<ProductPropertyViewModel, ProductProperty>(productPropertyViewModel);
                _productProperty.UpdateProductProperty(productProperty);
                await _saveChangesAsync.SaveChangesAsync();

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("Index", "UnImportantCategory");
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateChoiesPropertySubCategory()
        {
            int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            int subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("WhichCategory_SubCategoryId"));

            var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId);
            var categoryProductProperty = await _categoryProductProperty.GetCategoryProductPropertyByWhichCategoryId(whichCategory.Id);
            var productProperty = await _productProperty.GetProductPropertyByProductPropertyId(categoryProductProperty.ProductPropertyId);

            var model = _mapper.Map<ProductProperty, ProductPropertyViewModel>(productProperty);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateChoiesPropertySubCategory(ProductPropertyViewModel productPropertyViewModel)
        {
            try
                {
                var productProperty = _mapper.Map<ProductPropertyViewModel, ProductProperty>(productPropertyViewModel);
                _productProperty.UpdateProductProperty(productProperty);
                await _saveChangesAsync.SaveChangesAsync();

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("Index", "SubCategory");
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("Index", "Admin");
            }                 
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult ChoiesPropertyUnImportantCategory()
        {
            return View();
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChoiesPropertyUnImportantCategory(ProductPropertyViewModel productPropertyViewModel)
        {
            try
            {
                //ProductProperty
                //get user id create this work
                var userGuid = HttpContext.Session.GetString("UserGuid").ToString();
                Guid guid = new Guid(userGuid);
                productPropertyViewModel.UserCreatorId = guid;

                //which propert is select 
                var productProperty = _mapper.Map<ProductPropertyViewModel, ProductProperty>(productPropertyViewModel);
                var productProperties = await _productProperty.AddProductProperty(productProperty);

                //WhichCategory
                //create property from UnImportantCategory
                var unImportantId = Convert.ToInt32(HttpContext.Session.GetInt32("UnImportantCategoryId"));
                var whichCategoryViewModel = new WhichCategoryViewModel();
                whichCategoryViewModel.MainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                whichCategoryViewModel.CategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
                whichCategoryViewModel.SubCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
                whichCategoryViewModel.UnImportantCategoryId = unImportantId;

                var whichCategory = _mapper.Map<WhichCategoryViewModel, WhichCategory>(whichCategoryViewModel);
                var which = await _whichCategory.AddWhichCategory(whichCategory);
                await _saveChangesAsync.SaveChangesAsync();

                //insert whichcategoryId and ProductPropertyId in CategoryProductProperty Table
                var categoryProductPropertyViewModel = new CategoryProductPropertyViewModel()
                {
                    ProductPropertyId = productProperties.Id,
                    WhichCategoryId = which.Id
                };

                var categoryProductProperty = _mapper.Map<CategoryProductPropertyViewModel, CategoryProductProperty>(categoryProductPropertyViewModel);
                await _categoryProductProperty.AddCategoryProductProperty(categoryProductProperty);
                await _saveChangesAsync.SaveChangesAsync();

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("Index", "UnImportantCategory");

            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult ChoiesPropertySubCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChoiesPropertySubCategory(ProductPropertyViewModel productPropertyViewModel)
        {
            try
            {
                //ProductProperty
                //get user id create this work
                var userGuid = HttpContext.Session.GetString("UserGuid").ToString();
                Guid guid = new Guid(userGuid);
                productPropertyViewModel.UserCreatorId = guid;

                //which propert is select 
                var productProperty = _mapper.Map<ProductPropertyViewModel, ProductProperty>(productPropertyViewModel);
                var productProperties = await _productProperty.AddProductProperty(productProperty);

                //WhichCategory
                //create property from SubCategory
                var whichCategoryViewModel = new WhichCategoryViewModel();
                whichCategoryViewModel.MainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                whichCategoryViewModel.CategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
                whichCategoryViewModel.SubCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("WhichCategory_SubCategoryId"));
                whichCategoryViewModel.UnImportantCategoryId = 0;

                var whichCategory = _mapper.Map<WhichCategoryViewModel, WhichCategory>(whichCategoryViewModel);
                var which = await _whichCategory.AddWhichCategory(whichCategory);
                await _saveChangesAsync.SaveChangesAsync();

                //insert whichcategoryId and ProductPropertyId in CategoryProductProperty Table
                var categoryProductPropertyViewModel = new CategoryProductPropertyViewModel()
                {
                    ProductPropertyId = productProperties.Id,
                    WhichCategoryId = which.Id
                };

                var categoryProductProperty = _mapper.Map<CategoryProductPropertyViewModel, CategoryProductProperty>(categoryProductPropertyViewModel);
                await _categoryProductProperty.AddCategoryProductProperty(categoryProductProperty);
                await _saveChangesAsync.SaveChangesAsync();

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("Index", "SubCategory");

            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

    }
}
