using Core.Domain.Entity.Categories;

namespace Infrastructure.Interfaces.Categories
{
    public interface IMainCategory
    {
        public Task<List<MainCategory>> GetAllMainCategories();
        public Task<MainCategory> GetMainCategory(int id);
        public Task<MainCategory> AddMainCategory(MainCategory mainCategory);
        public MainCategory UpdateMainCategory(MainCategory mainCategory);
        public void DeleteMainCategory(MainCategory mainCategory);
    }
}
