using AutoMapper;
using Infrastructure.Interfaces.Categories;
using Microsoft.AspNetCore.Mvc;
using Core.ViewModel.Categories;
using Core.Domain.Entity.Categories;
using Ui.Areas.User.Model;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Core.Domain.Entity.Products;
using Core.ViewModel.Products;
using Infrastructure.Interfaces.Products;

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
        public async Task<IActionResult> Index()
        {
            var mainCategories = await _mainCategory.GetAllMainCategories();
            var categories = await _category.GetAllCategories();
            var subCategories = await _subCategory.GetAllSubCategories();
            var unImportantCategories = await _unImportantCategory.GetAllUnImportantCategories();

            var mainCategoryViewModels = _mapper.Map<List<MainCategory>, List<MainCategoryViewModel>>(mainCategories);
            var categoryViewModels = _mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            var subCategoryViewModels = _mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(subCategories);
            var unImportantCategoryViewModels = _mapper.Map<List<UnImportantCategory>, List<UnImportantCategoryViewModel>>(unImportantCategories);

            var allCategories = new List<AllCategories>();

            for (int i = 0; i < mainCategoryViewModels.Count(); i++)
            {
                var Categories = new AllCategories();

                Categories.MainCategoryName = mainCategoryViewModels[i].MainCategoryName;
                Categories.MainCategoryId = mainCategoryViewModels[i].Id;
                Categories.CategoryName = categoryViewModels[i].CategoryName;
                Categories.CategoryId = categoryViewModels[i].Id;
                Categories.SubCategoryName = subCategoryViewModels[i].SubCategoryName;
                Categories.SubCategoryId = subCategoryViewModels[i].Id;
                //check exist
                if (i <= unImportantCategoryViewModels.Count() - 1)
                {
                    Categories.UnImportantCategoryName = unImportantCategoryViewModels[i].UnImportantCategoryName;
                    Categories.UnImportantCategoryId = unImportantCategoryViewModels[i].Id;
                }
                allCategories.Add(Categories);
            }

            return View(allCategories);
        }

        #endregion

        #region Product

        [HttpGet]
        public async Task<IActionResult> Product(string mainCategoryId, string categoryId, string subCategoryId, string unImportantCategoryId)
        {
            WhichCategory whichCategoryId;
            if (string.IsNullOrEmpty(unImportantCategoryId))
            {
                whichCategoryId = await _whichCategory.GetWhichCategoryByIds(int.Parse(mainCategoryId), int.Parse(categoryId), int.Parse(subCategoryId));
            }
            else
            {
                whichCategoryId = await _whichCategory.GetWhichCategoryByIds(int.Parse(mainCategoryId), int.Parse(categoryId), int.Parse(subCategoryId), int.Parse(unImportantCategoryId));
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
        public async Task<IActionResult> ProductDetails(string id)
        {
            var product = await _products.GetProductById(long.Parse(id));
            var model = _mapper.Map<Product, ProductViewModel>(product);
            var images = await _images.GetImagesByProductId(product.Id);
            var imagesViewModels = _mapper.Map<List<Images>,List<ImagesViewModel>>(images);
            for (int i = 0; i < imagesViewModels.Count(); i++)
            {
                string a = imagesViewModels[i].ImageProduct;
                model.ImagesStrings.Add(a);

            }
            return View(model);
        }

        #endregion

    }
}
