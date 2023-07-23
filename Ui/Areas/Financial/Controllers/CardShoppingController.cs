using AutoMapper;
using Core.Domain.Entity.Card;
using Core.Domain.Entity.Products;
using Core.ViewModel.Products;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Card;
using Infrastructure.Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.HandShort;

namespace Ui.Areas.Financial.Controllers
{
    [Area("Financial")]
    [Authorize]
    public class CardShoppingController : Controller
    {

        #region Constructor

        private readonly IMapper _mapper;
        private readonly ICardShopping _cardShopping;
        private readonly IProducts _products;
        private readonly IImages _images;
        private readonly ISaveChangesAsync _saveChangesAsync;

        public CardShoppingController(IMapper mapper, ICardShopping cardShopping, IProducts products, IImages images, ISaveChangesAsync saveChangesAsync)
        {
            _mapper = mapper;
            _cardShopping = cardShopping;
            _products = products;
            _images = images;
            _saveChangesAsync = saveChangesAsync;
        }

        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userIdString = HttpContext.Session.GetString("UserGuid");
            Guid userId = new Guid(userIdString);

            var products = new List<Product>();
            var cardShoppings = await _cardShopping.GetCardShoppingByUserId(userId);
            foreach (var item in cardShoppings)
            {
                var product = await _products.GetProductById(item.ProductId);
                products.Add(product);

            }
            var models = _mapper.Map<List<Product>, List<ProductViewModel>>(products);

            var images = await _images.GetFirstImagesByProductId(products);
            var imagesViewModels = _mapper.Map<List<Images>, List<ImagesViewModel>>(images);
            for (int i = 0; i < imagesViewModels.Count(); i++)
            {
                models[i].ImageString = imagesViewModels[i].ImageProduct;
            }

            return View(models);
        }

        #endregion

        #region AddToCardShopping

        [HttpGet]
        public async Task<IActionResult> AddToCardShopping(long productId)
        {
            string userIdString = HttpContext.Session.GetString("UserGuid");
            Guid userId = new Guid(userIdString);

            var cardShopping = new CardShopping()
            {
                UserId = userId,
                ProductId = productId,
                Count = 1
            };
            var card = await _cardShopping.GetCardShoppingByUserIdAndProductId(userId, productId);
            if (card is not null)
            {
                TempData["Message"] = Extension.AlertDuplicateToCard();
                return RedirectToAction("ProductDetails", "Home", new { area = "User", productId = productId });
            }
            await _cardShopping.AddCardShopping(cardShopping);
            await _saveChangesAsync.SaveChangesAsync();
            TempData["Message"] = Extension.AlertAddToCard();

            return RedirectToAction("ProductDetails", "Home", new { area = "User", productId = productId });
        }

        #endregion

        #region DeleteToCardShopping

        [HttpGet]
        public async Task<IActionResult> DeleteToCardShopping(long productId)
        {
            string userIdString = HttpContext.Session.GetString("UserGuid");
            Guid userId = new Guid(userIdString);

            var cardShopping = await _cardShopping.GetCardShoppingByUserIdAndProductId(userId, productId);
            _cardShopping.DeleteCardShopping(cardShopping);
            await _saveChangesAsync.SaveChangesAsync();
            TempData["Message"] = Extension.AlertDeleteToCard();

            return RedirectToAction("Index");
        }

        #endregion

    }
}
