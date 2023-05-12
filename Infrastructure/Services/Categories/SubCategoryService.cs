using Core.Domain.Entity.Categories;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SubCategories
{
    public class SubCategoryService : ISubCategory
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public SubCategoryService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            var query = "SELECT * FROM SubCategories";
            using (var connection = _dapper.CreateConnection())
            {
                var SubCategories = await connection.QueryAsync<SubCategory>(query);
                return SubCategories.ToList();
            }
        }

        public async Task<List<SubCategory>> GetAllSubCategoriesByCategoriesId(int categoryId)
        {
            var query = $"SELECT [Id],[SubCategoryName],[CategoryId] FROM SubCategories WHERE CategoryId = {categoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                var SubCategories = await connection.QueryAsync<SubCategory>(query);
                return SubCategories.ToList();
            }
        }

        public async Task<SubCategory> GetSubCategory(int id)
        {
            var query = $"SELECT [Id],[SubCategoryName],[CategoryId] FROM SubCategories WHERE Id = {id}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<SubCategory>(query);
            }
        }

        public async Task<SubCategory> AddSubCategory(SubCategory SubCategory)
        {
            await _db.SubCategories.AddAsync(SubCategory);
            return SubCategory;
        }

        public SubCategory UpdateSubCategory(SubCategory SubCategory)
        {
            _db.Entry(SubCategory).State = EntityState.Modified;
            return SubCategory;
        }

        public void DeleteSubCategory(SubCategory SubCategory)
        {
            _db.Entry(SubCategory).State = EntityState.Deleted;
        }
    }
}
