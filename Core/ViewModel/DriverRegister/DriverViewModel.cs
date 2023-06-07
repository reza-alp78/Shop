using Core.ViewModel.Products;

namespace Core.ViewModel.DriverRegister
{
    public class DriverViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
    }
}
