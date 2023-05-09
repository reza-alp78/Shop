using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.DriverRegister;
using Core.Enum;
using Core.IdentityEntity;

namespace Core.Domain.Entity.Products
{
    public class Buy
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public long ProductRegisterId { get; set; }
        public int Count { get; set; }
        public StepBuyEnum StepBuyEnum { get; set; }
        public int DriverId { get; set; }
        public long ConsignmentNumber { get; set; }
        public DateTime DateTime { get; set; }

        public Driver Driver { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ProductRegister ProductRegister { get; set; }
    }
}
