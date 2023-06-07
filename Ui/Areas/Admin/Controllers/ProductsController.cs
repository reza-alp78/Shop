using AutoMapper;
using Infrastructure.Interfaces.Categories;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Infrastructure.Interfaces.Products;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Core.IdentityEntity;
using Core.ViewModel.Products;
using Core.Domain.Entity.Products;
using Ui.HandShort;
using Core.ViewModel.CategoriesAndProducts;
using Core.Domain.Entity.CategoriesAndProducts;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        #region Constructor

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProducts _products;
        private readonly IImages _images;
        private readonly IProductProperty _productProperty;
        private readonly IProductRegister _productRegister;
        private readonly IWhichCategory _whichCategory;
        private readonly ICategoryProductProperty _categoryProductProperty;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public ProductsController(UserManager<ApplicationUser> userManager, IProducts products, IImages images, IProductProperty productProperty, IProductRegister productRegister, IWhichCategory whichCategory, ICategoryProductProperty categoryProductProperty, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _userManager = userManager;
            _userManager = userManager;
            _products = products;
            _images = images;
            _productProperty = productProperty;
            _productRegister = productRegister;
            _whichCategory = whichCategory;
            _categoryProductProperty = categoryProductProperty;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> IndexProductForSubCategory(string subCategoryId)
        {
            //when model create back to index and we not subcategory id
            if (!string.IsNullOrEmpty(subCategoryId))
            {
                //when model create back to index and we not subcategory id
                HttpContext.Session.SetInt32("SubCategoryId", Convert.ToInt32(subCategoryId));

                //Get whichCategory Id
                int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));

                var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, Convert.ToInt32(subCategoryId));
                HttpContext.Session.SetInt32("WhichCategory", whichCategory.Id);

            }


            string userId = HttpContext.Session.GetString("UserGuid");
            Guid guid = new Guid(userId);

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            int whichCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("WhichCategory"));

            //if admin entery show all products
            foreach (var role in roles)
            {
                if (role == "Admin")
                {
                    var AllProducts = await _products.GetAllProductsByWhichCategoryId(whichCategoryId);
                    var products = _mapper.Map<List<Product>, List<ProductViewModel>>(AllProducts);
                    return View(products);
                }
            }

            var UserProducts = await _products.GetAllProductsByWhichCategoryIdAndByUserCretorId(whichCategoryId, guid);
            var models = _mapper.Map<List<Product>, List<ProductViewModel>>(UserProducts);
            return View(models);
        }


        [HttpGet]
        public async Task<IActionResult> IndexProductForUnImportantCategory(string unImportantCategoryId)
        {
            //when model create back to index and we not unImportantCategory Id
            if (!string.IsNullOrEmpty(unImportantCategoryId))
            {
                //when model create back to index and we not unImportantCategory Id
                HttpContext.Session.SetInt32("UnImportantCategoryId", int.Parse(unImportantCategoryId));
                //Get whichCategory Id
                int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
                int subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));

                var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId, int.Parse(unImportantCategoryId));
                HttpContext.Session.SetInt32("WhichCategory", whichCategory.Id);
            }


            string userId = HttpContext.Session.GetString("UserGuid");
            Guid guid = new Guid(userId);

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            int whichCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("WhichCategory"));

            //if admin entery show all products
            foreach (var role in roles)
            {
                if (role == "Admin")
                {
                    var AllProducts = await _products.GetAllProductsByWhichCategoryId(whichCategoryId);
                    var products = _mapper.Map<List<Product>, List<ProductViewModel>>(AllProducts);
                    return View(products);
                }
            }

            var UserProducts = await _products.GetAllProductsByWhichCategoryIdAndByUserCretorId(whichCategoryId, guid);
            var models = _mapper.Map<List<Product>, List<ProductViewModel>>(UserProducts);
            return View(models);
        }

        #endregion

        #region Add

        #region AddProductForSubCategory

        [HttpGet]
        public async Task<IActionResult> AddProductForSubCategory()
        {
            try
            {
                //get ProductProperty for subCategory
                int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
                int subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));

                var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId);
                HttpContext.Session.SetInt32("WhichCategory", whichCategory.Id);
                var categoryProductProperty = await _categoryProductProperty.GetCategoryProductPropertyByWhichCategoryId(whichCategory.Id);
                var productProperty = await _productProperty.GetProductPropertyByProductPropertyId(categoryProductProperty.ProductPropertyId);

                string userId = HttpContext.Session.GetString("UserGuid");
                Guid guid = new Guid(userId);
                productProperty.UserCreatorId = guid;

                var models = _mapper.Map<ProductProperty, ProductPropertyViewModel>(productProperty);
                return View(models);
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("IndexProductForSubCategory");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductForSubCategory(ProductPropertyViewModel productPropertyViewModel, List<IFormFile> Images)
        {
            try
            {
                //products save
                if (Images.Count() == 0)
                {
                    TempData["Message"] = "لطفا تصویری انتخاب کنید";
                    return View(productPropertyViewModel);
                }
                for (int i = 0; i < Images.Count(); i++)
                {
                    if (Path.GetExtension(Images[i].FileName).ToLower() != ".jpg" && Path.GetExtension(Images[i].FileName).ToLower() != ".png"
                        && Path.GetExtension(Images[i].FileName).ToLower() != ".gif" && Path.GetExtension(Images[i].FileName).ToLower() != ".jpeg"
                            && Images[i].Length > 0)
                    {
                        TempData["Message"] = "فایل انتخواب شده درست نمیباشد";
                        return View(productPropertyViewModel);
                    }
                }

                var model = _mapper.Map<ProductPropertyViewModel, Product>(productPropertyViewModel);
                var product = await _products.AddProduct(model);
                if (product == null)
                {
                    TempData["Message"] = Extension.AlertError();
                    return RedirectToAction("IndexProductForSubCategory");
                }

                await _saveChangesAsync.SaveChangesAsync();
                //images save
                try
                {
                    foreach (var Image in Images)
                    {
                        await using (var stream = new MemoryStream())
                        {
                            var imagesViewModel = new ImagesViewModel();
                            await Image.CopyToAsync(stream);
                            imagesViewModel.ImageProduct = stream.ToArray();
                            imagesViewModel.ProductId = product.Id;
                            var imgs = _mapper.Map<ImagesViewModel, Images>(imagesViewModel);
                            await _images.AddImages(imgs);
                        }
                    }
                    //productRegister : mean => pruduct in which Category
                    int whichCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("WhichCategory"));
                    var categoryProductProperty = await _categoryProductProperty.GetCategoryProductPropertyByWhichCategoryId(whichCategoryId);
                    var productRegisterViewModel = new ProductRegisterViewModel()
                    {
                        ProductId = product.Id,
                        CategoryProductPropertyId = categoryProductProperty.Id,
                    };
                    var productRegister = _mapper.Map<ProductRegisterViewModel, ProductRegister>(productRegisterViewModel);
                    await _productRegister.AddProductRegister(productRegister);

                    await _saveChangesAsync.SaveChangesAsync();

                    TempData["Message"] = Extension.AlertSuccess();
                    return RedirectToAction("IndexProductForSubCategory");

                }
                catch (Exception)
                {
                    //delete product and alert error
                    return RedirectToAction("DeleteProduct", new { productId = product.Id });
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("IndexProductForSubCategory");
            }
        }

        #endregion

        #region AddProductForUnImportantCategory

        [HttpGet]
        public async Task<IActionResult> AddProductForUnImportantCategory()
        {
            try
            {
                //get ProductProperty for UnImportantCategory
                int mainCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("MainCategoryId"));
                int categoryId = Convert.ToInt32(HttpContext.Session.GetInt32("CategoryId"));
                int subCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("SubCategoryId"));
                int unImportantCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("UnImportantCategoryId"));

                var whichCategory = await _whichCategory.GetWhichCategoryByIds(mainCategoryId, categoryId, subCategoryId, unImportantCategoryId);
                HttpContext.Session.SetInt32("WhichCategory", whichCategory.Id);
                var categoryProductProperty = await _categoryProductProperty.GetCategoryProductPropertyByWhichCategoryId(whichCategory.Id);
                var productProperty = await _productProperty.GetProductPropertyByProductPropertyId(categoryProductProperty.ProductPropertyId);

                string userId = HttpContext.Session.GetString("UserGuid");
                Guid guid = new Guid(userId);
                productProperty.UserCreatorId = guid;

                var models = _mapper.Map<ProductProperty, ProductPropertyViewModel>(productProperty);
                return View(models);
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("IndexProductForUnImportantCategory");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductForUnImportantCategory(ProductPropertyViewModel productPropertyViewModel, List<IFormFile> Images)
        {
            try
            {

                //products save
                if (Images.Count() == 0)
                {
                    TempData["Message"] = "لطفا تصویری انتخاب کنید";
                    return View(productPropertyViewModel);
                }
                for (int i = 0; i < Images.Count(); i++)
                {
                    if (Path.GetExtension(Images[i].FileName).ToLower() != ".jpg" && Path.GetExtension(Images[i].FileName).ToLower() != ".png"
                        && Path.GetExtension(Images[i].FileName).ToLower() != ".gif" && Path.GetExtension(Images[i].FileName).ToLower() != ".jpeg"
                            && Images[i].Length > 0)
                    {
                        TempData["Message"] = "فایل انتخواب شده درست نمیباشد";
                        return View(productPropertyViewModel);
                    }
                }

                var model = _mapper.Map<ProductPropertyViewModel, Product>(productPropertyViewModel);
                var product = await _products.AddProduct(model);
                if (product == null)
                {
                    TempData["Message"] = Extension.AlertError();
                    return RedirectToAction("IndexProductForUnImportantCategory");
                }

                await _saveChangesAsync.SaveChangesAsync();
                //images save
                try
                {
                    foreach (var Image in Images)
                    {
                        await using (var stream = new MemoryStream())
                        {
                            var imagesViewModel = new ImagesViewModel();
                            await Image.CopyToAsync(stream);
                            imagesViewModel.ImageProduct = stream.ToArray();
                            imagesViewModel.ProductId = product.Id;
                            var imgs = _mapper.Map<ImagesViewModel, Images>(imagesViewModel);
                            await _images.AddImages(imgs);
                        }
                    }
                    //productRegister : mean => pruduct in which Category
                    int whichCategoryId = Convert.ToInt32(HttpContext.Session.GetInt32("WhichCategory"));
                    var categoryProductProperty = await _categoryProductProperty.GetCategoryProductPropertyByWhichCategoryId(whichCategoryId);
                    var productRegisterViewModel = new ProductRegisterViewModel()
                    {
                        ProductId = product.Id,
                        CategoryProductPropertyId = categoryProductProperty.Id,
                    };
                    var productRegister = _mapper.Map<ProductRegisterViewModel, ProductRegister>(productRegisterViewModel);
                    await _productRegister.AddProductRegister(productRegister);

                    await _saveChangesAsync.SaveChangesAsync();

                    TempData["Message"] = Extension.AlertSuccess();
                    return RedirectToAction("IndexProductForUnImportantCategory");

                }
                catch (Exception)
                {
                    //delete product and alert error
                    return RedirectToAction("DeleteProduct", new { productId = product.Id });
                }
            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("IndexProductForUnImportantCategory");
            }
        }

        #endregion

        #endregion

        #region Update

        #region UpdateProductForSubCategory

        [HttpGet]
        public async Task<IActionResult> UpdateProductForSubCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Message"] = Extension.AlertNotFound();
                return RedirectToAction("IndexProductForSubCategory");
            }
            var product = await _products.GetProductById(long.Parse(id));
            var model = _mapper.Map<Product, ProductViewModel>(product);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductForSubCategory(ProductViewModel productViewModel, List<IFormFile> Images)
        {
            try
            {
                if (Images.Count() == 0)
                {
                    TempData["Message"] = "لطفا تصویری انتخاب کنید";
                    return View(productViewModel);
                }
                for (int i = 0; i < Images.Count(); i++)
                {
                    if (Path.GetExtension(Images[i].FileName).ToLower() != ".jpg" && Path.GetExtension(Images[i].FileName).ToLower() != ".png"
                        && Path.GetExtension(Images[i].FileName).ToLower() != ".gif" && Path.GetExtension(Images[i].FileName).ToLower() != ".jpeg"
                            && Images[i].Length > 0)
                    {
                        TempData["Message"] = "فایل انتخواب شده درست نمیباشد";
                        return View(productViewModel);
                    }
                }

                var model = _mapper.Map<ProductViewModel, Product>(productViewModel);
                var product = _products.UpdateProduct(model);
                if (product == null)
                {
                    TempData["Message"] = Extension.AlertError();
                    return RedirectToAction("IndexProductForSubCategory");
                }

                //images save

                var images = await _images.GetImagesByProductId(model.Id);
                foreach (var item in images)
                {
                    _images.DeleteImages(item);
                }
                foreach (var Image in Images)
                {
                    await using (var stream = new MemoryStream())
                    {
                        var imagesViewModel = new ImagesViewModel();
                        await Image.CopyToAsync(stream);
                        imagesViewModel.ImageProduct = stream.ToArray();
                        imagesViewModel.ProductId = product.Id;
                        var imgs = _mapper.Map<ImagesViewModel, Images>(imagesViewModel);
                        await _images.AddImages(imgs);
                    }
                }

                await _saveChangesAsync.SaveChangesAsync();

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("IndexProductForSubCategory");

            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("IndexProductForSubCategory");
            }
        }
        #endregion

        #region UpdateProductForUnImportantCategory

        [HttpGet]
        public async Task<IActionResult> UpdateProductForUnImportantCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Message"] = Extension.AlertNotFound();
                return RedirectToAction("IndexProductForSubCategory");
            }
            var product = await _products.GetProductById(long.Parse(id));
            var model = _mapper.Map<Product, ProductViewModel>(product);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductForUnImportantCategory(ProductViewModel productViewModel, List<IFormFile> Images)
        {
            try
            {
                if (Images.Count() == 0)
                {
                    TempData["Message"] = "لطفا تصویری انتخاب کنید";
                    return View(productViewModel);
                }
                for (int i = 0; i < Images.Count(); i++)
                {
                    if (Path.GetExtension(Images[i].FileName).ToLower() != ".jpg" && Path.GetExtension(Images[i].FileName).ToLower() != ".png"
                        && Path.GetExtension(Images[i].FileName).ToLower() != ".gif" && Path.GetExtension(Images[i].FileName).ToLower() != ".jpeg"
                            && Images[i].Length > 0)
                    {
                        TempData["Message"] = "فایل انتخواب شده درست نمیباشد";
                        return View(productViewModel);
                    }
                }

                var model = _mapper.Map<ProductViewModel, Product>(productViewModel);
                var product = _products.UpdateProduct(model);
                if (product == null)
                {
                    TempData["Message"] = Extension.AlertError();
                    return RedirectToAction("IndexProductForUnImportantCategory");
                }

                //images save

                var images = await _images.GetImagesByProductId(model.Id);
                foreach (var item in images)
                {
                    _images.DeleteImages(item);
                }
                foreach (var Image in Images)
                {
                    await using (var stream = new MemoryStream())
                    {
                        var imagesViewModel = new ImagesViewModel();
                        await Image.CopyToAsync(stream);
                        imagesViewModel.ImageProduct = stream.ToArray();
                        imagesViewModel.ProductId = product.Id;
                        var imgs = _mapper.Map<ImagesViewModel, Images>(imagesViewModel);
                        await _images.AddImages(imgs);
                    }
                }

                await _saveChangesAsync.SaveChangesAsync();

                TempData["Message"] = Extension.AlertSuccess();
                return RedirectToAction("IndexProductForUnImportantCategory");

            }
            catch (Exception)
            {
                TempData["Message"] = Extension.AlertUnKnown();
                return RedirectToAction("IndexProductForUnImportantCategory");
            }
        }

        #endregion

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> DeleteProductSubCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("IndexProductForSubCategory");

            }
            //delete product and delet imagesProduct
            var product = await _products.GetProductById(long.Parse(id));
            var images = await _images.GetImagesByProductId(product.Id);
            foreach (var item in images)
            {
                _images.DeleteImages(item);
            }
            _products.DeleteProduct(product);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("IndexProductForSubCategory");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProductUnImportantCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Message"] = Extension.AlertError();
                return RedirectToAction("IndexProductForUnImportantCategory");

            }
            //delete product and delet imagesProduct
            var product = await _products.GetProductById(long.Parse(id));
            var images = await _images.GetImagesByProductId(product.Id);
            foreach (var item in images)
            {
                _images.DeleteImages(item);
            }
            _products.DeleteProduct(product);
            await _saveChangesAsync.SaveChangesAsync();

            TempData["Message"] = Extension.AlertSuccess();
            return RedirectToAction("IndexProductForUnImportantCategory");
        }

        //when error after create prudact and befor create images  means remove product whitout save images
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            //delete product and delet imagesProduct
            var prdct = await _products.GetProductById(productId);
            var allImages = await _images.GetImagesByProductId(prdct.Id);
            foreach (var item in allImages)
            {
                _images.DeleteImages(item);
            }
            _products.DeleteProduct(prdct);
            await _saveChangesAsync.SaveChangesAsync();
            TempData["Message"] = Extension.AlertError();
            return RedirectToAction("IndexProductForSubCategory");
        }

        #endregion

    }
}
