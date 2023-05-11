using Core.Domain.Entity.Categories;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Categories
{
    public class MainCategoryService : IMainCategory
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public MainCategoryService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<MainCategory>> GetAllMainCategories()
        {
            var query = "SELECT * FROM MainCategories";
            using (var connection = _dapper.CreateConnection())
            {
                var mainCategories = await connection.QueryAsync<MainCategory>(query);
                return mainCategories.ToList();
            }
        }

        public async Task<MainCategory> GetMainCategory(int id)
        {
            var query = $"SELECT [Id],[MainCategoryName] FROM MainCategories WHERE Id = {id}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<MainCategory>(query);
            }
        }

        public async Task<MainCategory> AddMainCategory(MainCategory mainCategory)
        {
            await _db.MainCategories.AddAsync(mainCategory);
            return mainCategory;
        }

        public MainCategory UpdateMainCategory(MainCategory mainCategory)
        {
            _db.Entry(mainCategory).State = EntityState.Modified;
            return mainCategory;
        }

        public void DeleteMainCategory(MainCategory mainCategory)
        {
            _db.Entry(mainCategory).State = EntityState.Deleted;
        }
    }
}
