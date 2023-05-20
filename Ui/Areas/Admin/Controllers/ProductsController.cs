using AutoMapper;
using Infrastructure.Interfaces.Categories;
using Infrastructure.Interfaces.CategoriesAndProducts;
using Infrastructure.Interfaces.Products;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {

        #region Constructor

        private readonly IProductProperty _productProperty;
        private readonly IWhichCategory _whichCategory;
        private readonly ICategoryProductProperty _categoryProductProperty;
        private readonly ISaveChangesAsync _saveChangesAsync;
        private readonly IMapper _mapper;

        public ProductsController(IProductProperty productProperty, IWhichCategory whichCategory, ICategoryProductProperty categoryProductProperty, ISaveChangesAsync saveChangesAsync, IMapper mapper)
        {
            _productProperty = productProperty;
            _whichCategory = whichCategory;
            _categoryProductProperty = categoryProductProperty;
            _saveChangesAsync = saveChangesAsync;
            _mapper = mapper;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
