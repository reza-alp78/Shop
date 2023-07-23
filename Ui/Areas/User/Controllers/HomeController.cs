using AutoMapper;
using Infrastructure.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;
using Core.ViewModel.Categories;
using Core.Domain.Entity.Categories;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Core.Domain.Entity.Products;
using Core.ViewModel.Products;
using Infrastructure.Interfaces.Products;
using Ui.HandShort;

namespace Ui.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {

        #region Constructor

        private readonly ICategory _category;
        private readonly IProducts _products;
        private readonly IImages _images;
        private readonly IWhichCategory _whichCategory;
        private readonly IProductRegister _productRegister;
        private readonly IMainCategory _mainCategory;
        private readonly IUnImportantCategory _unImportantCategory;
        private readonly ISubCategory _subCategory;
        private readonly IMapper _mapper;

        public HomeController(ICategory category, IProducts products, IImages images, IWhichCategory whichCategory, IProductRegister productRegister, IMainCategory mainCategory, IUnImportantCategory unImportantCategory, ISubCategory subCategory, IMapper mapper)
        {
            _category = category;
            _products = products;
            _images = images;
            _whichCategory = whichCategory;
            _productRegister = productRegister;
            _mainCategory = mainCategory;
            _unImportantCategory = unImportantCategory;
            _subCategory = subCategory;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> MainCategoryIndex()
        {
            var mainCategories = await _mainCategory.GetAllMainCategories();

            var models = _mapper.Map<List<MainCategory>, List<MainCategoryViewModel>>(mainCategories);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryIndex(int mainCategoryId)
        {
            if (mainCategoryId == 0)
            {
                mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            }
            HttpContext.Session.SetInt32("MainCategoryId", mainCategoryId);
            var categories = await _category.GetAllCategoriesByMainCategoriesId(mainCategoryId);

            var models = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> SubCategoryIndex(int categoryId)
        {
            if (categoryId == 0)
            {
                categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            }
            HttpContext.Session.SetInt32("CategoryId", categoryId);
            var subCategories = await _subCategory.GetAllSubCategoriesByCategoriesId(categoryId);

            var models = _mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(subCategories);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> UnImportantCategoryIndex(int subCategoryId)
        {
            if (subCategoryId == 0)
            {
                subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
            }
            HttpContext.Session.SetInt32("SubCategoryId", subCategoryId);
            var unImportantCategories = await _unImportantCategory.GetAllUnImportantCategoriesBySubCategoriesId(subCategoryId);

            var models = _mapper.Map<List<UnImportantCategory>, List<UnImportantCategoryViewModel>>(unImportantCategories);

            return View(models);
        }

        #endregion

        #region Product

        [HttpGet]
        public async Task<IActionResult> Product(int subCategoryId, int unImportantCategoryId)
        {
            int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
            int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
            
            WhichCategory whichCategoryId;
            if (unImportantCategoryId is 0 && subCategoryId is not 0)
            {
                whichCategoryId = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId);
            }
            else if (subCategoryId is 0 && unImportantCategoryId is not 0)
            {
                int _subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
                whichCategoryId = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, _subCategoryId, unImportantCategoryId);
            }
            else
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("MainCategoryIndex");
            }
            if (whichCategoryId is null)
            {
                return View();
            }
            var products = await _productRegister.GetAllProductsByWhichCategoryId(whichCategoryId.Id);
            var models = _mapper.Map<List<Product>, List<ProductViewModel>>(products);
            //return first images from each Products
            var images = await _images.GetFirstImagesByProductId(products);
            for (int i = 0; i < models.Count(); i++)
            {
                models[i].ImageString = images[i].ImageProduct;
            }
            return View(models);
        }

        #endregion

        #region ProductDetails

        [HttpGet]
        public async Task<IActionResult> ProductDetails(long productId)
        {
            var product = await _products.GetProductById(productId);
            var model = _mapper.Map<Product, ProductViewModel>(product);
            var images = await _images.GetImagesByProductId(product.Id);
            var imagesViewModels = _mapper.Map<List<Images>, List<ImagesViewModel>>(images);
            for (int i = 0; i < imagesViewModels.Count(); i++)
            {
                string img = imagesViewModels[i].ImageProduct;
                model.ImagesStrings.Add(img);

            }
            return View(model);
        }

        #endregion

    }
}
