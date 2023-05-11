using Core.Domain.Entity.Categories;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Categories
{
    public class CategoryService : ICategory
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public CategoryService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<Category>> GetAllCategories()
        {
            var query = "SELECT * FROM Categories";
            using (var connection = _dapper.CreateConnection())
            {
                var mainCategories = await connection.QueryAsync<Category>(query);
                return mainCategories.ToList();
            }
        }

        public async Task<List<Category>> GetAllCategoriesByMainCategoriesId(int mainCategoryId)
        {
            var query = $"SELECT [Id],[CategoryName],[MainCategoryId] FROM Categories WHERE MainCategoryId = {mainCategoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                var mainCategories = await connection.QueryAsync<Category>(query);
                return mainCategories.ToList();
            }
        }

        public async Task<Category> GetCategory(int id)
        {
            var query = $"SELECT [Id],[CategoryName],[MainCategoryId] FROM Categories WHERE Id = {id}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Category>(query);
            }
        }

        public async Task<Category> AddCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            return category;
        }

        public void DeleteCategory(Category category)
        {
            _db.Entry(category).State = EntityState.Deleted;
        }
       
    }
}
