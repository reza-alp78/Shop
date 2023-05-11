using Core.Domain.Entity.Categories;

namespace Infrastructure.Interfaces.Categories
{
    public interface ICategory
    {
        public Task<List<Category>> GetAllCategories();
        public Task<List<Category>> GetAllCategoriesByMainCategoriesId(int mainCategoryId);
        public Task<Category> GetCategory(int id);
        public Task<Category> AddCategory(Category category);
        public Category UpdateCategory(Category category);
        public void DeleteCategory(Category category);
    }
}
