using Core.Domain.Entity.Categories;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Categories
{
    public class UnImportantCategoryService : IUnImportantCategory
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public UnImportantCategoryService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<UnImportantCategory>> GetAllUnImportantCategories()
        {
            var query = "SELECT * FROM UnImportantCategories";
            using (var connection = _dapper.CreateConnection())
            {
                var unImportantCategories = await connection.QueryAsync<UnImportantCategory>(query);
                return unImportantCategories.ToList();
            }
        }

        public async Task<List<UnImportantCategory>> GetAllUnImportantCategoriesBySubCategoriesId(int subCategoryId)
        {
            var query = $"SELECT [Id],[UnImportantCategoryName],[SubCategoryId] FROM UnImportantCategories WHERE SubCategoryId = {subCategoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                var unImportantCategories = await connection.QueryAsync<UnImportantCategory>(query);
                return unImportantCategories.ToList();
            }
        }

        public async Task<UnImportantCategory> GetUnImportantCategory(int id)
        {
            var query = $"SELECT [Id],[UnImportantCategoryName],[SubCategoryId] FROM UnImportantCategories WHERE Id = {id}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UnImportantCategory>(query);
            }
        }

        public async Task<UnImportantCategory> AddUnImportantCategory(UnImportantCategory unImportantCategory)
        {
            await _db.UnImportantCategories.AddAsync(unImportantCategory);
            return unImportantCategory;
        }

        public UnImportantCategory UpdateUnImportantCategory(UnImportantCategory unImportantCategory)
        {
            _db.Entry(unImportantCategory).State = EntityState.Modified;
            return unImportantCategory;
        }

        public void DeleteUnImportantCategory(UnImportantCategory unImportantCategory)
        {
            _db.Entry(unImportantCategory).State = EntityState.Deleted;
        }

    }
}
