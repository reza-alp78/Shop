using Core.Domain.Entity.Products;

namespace Core.Domain.Entity.CategoriesAndProducts
{
    public class ProductRegister
    {
        public long Id { get; set; }
        public int CategoryProductPropertyId { get; set; }
        public CategoryProductProperty CategoryProductProperty { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public List<Buy> Buys { get; set; }
    }
}
