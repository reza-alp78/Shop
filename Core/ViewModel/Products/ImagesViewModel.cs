namespace Core.ViewModel.Products
{
    public class ImagesViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte[] ImageProduct { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
    }
}
