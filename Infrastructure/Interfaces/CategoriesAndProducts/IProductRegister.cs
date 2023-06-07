using Core.Domain.Entity.CategoriesAndProducts;

namespace Infrastructure.Interfaces.CategoriesAndProducts
{
    public interface IProductRegister
    {
        public Task<ProductRegister> AddProductRegister(ProductRegister productRegister);
        public ProductRegister UpdateCategoryProductProperty(ProductRegister productRegister);
        public void DeleteCategoryProductProperty(ProductRegister productRegister);
    }
}
