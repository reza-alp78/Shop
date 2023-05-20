using Core.Domain.Entity.Products;

namespace Core.Domain.Entity.DriverRegister
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}
