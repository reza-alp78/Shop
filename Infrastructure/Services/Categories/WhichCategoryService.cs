using Core.Domain.Entity.Categories;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Categories
{
    public class WhichCategoryService : IWhichCategory
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public WhichCategoryService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<WhichCategory> AddWhichCategory(WhichCategory whichCategory)
        {
            await _db.WhichCategories.AddAsync(whichCategory);
            return whichCategory;
        }

        public async Task<WhichCategory> GetWhichCategoryByIds(int mainCategoryId, int categoryId, int subcategoryId)
        {
            var query = $"SELECT [Id],[MainCategoryId],[CategoryId],[SubCategoryId],[UnImportantCategoryId] FROM WhichCategories WHERE " +
                $"MainCategoryId = {mainCategoryId} and CategoryId = {categoryId} and SubCategoryId = {subcategoryId} and UnImportantCategoryId = 0";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<WhichCategory>(query);
            }
        }

        public async Task<WhichCategory> GetWhichCategoryByIds(int mainCategoryId, int categoryId, int subcategoryId, int unImportantCategoryId)
        {
            var query = $"SELECT [Id],[MainCategoryId],[CategoryId],[SubCategoryId],[UnImportantCategoryId] FROM WhichCategories WHERE " +
                $"MainCategoryId = {mainCategoryId} and CategoryId = {categoryId} and SubCategoryId = {subcategoryId} and UnImportantCategoryId = {unImportantCategoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<WhichCategory>(query);
            }
        }

        public WhichCategory UpdateWhichCategory(WhichCategory whichCategory)
        {
            _db.Entry(whichCategory).State = EntityState.Modified;
            return whichCategory;
        }

        public void DeleteWhichCategory(WhichCategory whichCategory)
        {
            _db.Entry(whichCategory).State = EntityState.Deleted;
        }

    }
}
