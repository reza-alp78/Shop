using Core.Domain.Entity.Categories;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Categories
{
    public class CategoryService : ICategory
    {
        private readonly ShopDbContext _db;

        public CategoryService(ShopDbContext shopDbContext)
        {
            _db = shopDbContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategory(Category Category)
        {
            await _db.Categories.AddAsync(Category);
            return Category;
        }

        public Category UpdateCategory(Category Category)
        {
            _db.Entry(Category).State = EntityState.Modified;
            return Category;
        }

        public void DeleteCategory(Category Category)
        {
            _db.Entry(Category).State = EntityState.Deleted;
        }
    }
}
