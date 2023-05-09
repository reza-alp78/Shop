using Core.Domain.Entity.Products;

namespace Core.Domain.Entity.CategoriesAndProducts
{
    public class ProductRegister
    {
        public long Id { get; set; }
        public int WhichCategoryId { get; set; }
        public long ProductId { get; set; }

        public List<Buy> Buys { get; set; }
    }
}
