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
                var subCategories = await connection.QueryAsync<SubCategory>(query);
                return subCategories.ToList();
            }
        }

        public async Task<List<SubCategory>> GetAllSubCategoriesByCategoriesId(int categoryId)
        {
            var query = $"SELECT [Id],[SubCategoryName],[CategoryId] FROM SubCategories WHERE CategoryId = {categoryId}";
            using (var connection = _dapper.CreateConnection())
            {
                var subCategories = await connection.QueryAsync<SubCategory>(query);
                return subCategories.ToList();
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

        public async Task<SubCategory> AddSubCategory(SubCategory subCategory)
        {
            await _db.SubCategories.AddAsync(subCategory);
            return subCategory;
        }

        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
            _db.Entry(subCategory).State = EntityState.Modified;
            return subCategory;
        }

        public void DeleteSubCategory(SubCategory subCategory)
        {
            _db.Entry(subCategory).State = EntityState.Deleted;
        }
    }
}
