using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.Products;

namespace Infrastructure.Interfaces.CategoriesAndProducts
{
    public interface IProductRegister
    {
        public Task<List<Product>> GetAllProductsByWhichCategoryId(int whichCategoryId);
        public Task<ProductRegister> AddProductRegister(ProductRegister productRegister);
        public ProductRegister UpdateCategoryProductProperty(ProductRegister productRegister);
        public void DeleteCategoryProductProperty(ProductRegister productRegister);
    }
}
