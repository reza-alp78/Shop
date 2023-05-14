using Core.Domain.Entity.CategoriesAndProducts;

namespace Infrastructure.Interfaces.CategoriesAndProducts
{
    public interface ICategoryProductProperty
    {
        public Task<CategoryProductProperty> AddCategoryProductProperty(CategoryProductProperty categoryProductProperty);
        public Task<CategoryProductProperty> GetCategoryProductPropertyByWhichCategoryId(int whichCategoryId);
        public CategoryProductProperty UpdateCategoryProductProperty(CategoryProductProperty categoryProductProperty);
        public void DeleteCategoryProductProperty(CategoryProductProperty categoryProductProperty);
    }
}
