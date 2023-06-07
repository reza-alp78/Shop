using Core.ViewModel.Products;

namespace Core.ViewModel.CategoriesAndProducts
{
    public class ProductRegisterViewModel
    {
        public long Id { get; set; }
        public int CategoryProductPropertyId { get; set; }
        public long ProductId { get; set; }

        public List<BuyViewModel> BuyViewModels { get; set; }
    }
}
