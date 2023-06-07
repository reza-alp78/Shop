using Core.Enum;
using Core.IdentityEntity;
using Core.ViewModel.CategoriesAndProducts;
using Core.ViewModel.DriverRegister;

namespace Core.ViewModel.Products
{
    public class BuyViewModel
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public long ProductRegisterId { get; set; }
        public int Count { get; set; }
        public StepBuyEnum StepBuyEnum { get; set; }
        public int DriverId { get; set; }
        public long ConsignmentNumber { get; set; }
        public DateTime DateTime { get; set; }

        public DriverViewModel DriverViewMolde { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ProductRegisterViewModel ProductRegisterViewModel { get; set; }
    }
}
