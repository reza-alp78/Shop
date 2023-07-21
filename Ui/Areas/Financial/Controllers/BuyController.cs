using AutoMapper;
using Core.Domain.Entity.Products;
using Core.ViewModel.Products;
using Infrastructure.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Financial.Controllers
{
    [Area("Financial")]
    public class BuyController : Controller
    {

        #region Constructor

        private readonly IProducts _products;
        private readonly IMapper _mapper;

        public BuyController(IProducts products, IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Buy(long productId)
        {
            var product = await _products.GetProductById(productId);
            var model = _mapper.Map<Product, ProductViewModel>(product);
            return View(model);
        }
    }
}
