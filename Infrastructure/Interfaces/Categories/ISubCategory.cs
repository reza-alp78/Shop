using Core.Domain.Entity.Categories;

namespace Infrastructure.Interfaces.Categories
{
    public interface ISubCategory
    {
        public Task<List<SubCategory>> GetAllSubCategories();
        public Task<List<SubCategory>> GetAllSubCategoriesByCategoriesId(int categoryId);
        public Task<SubCategory> GetSubCategory(int id);
        public Task<SubCategory> AddSubCategory(SubCategory SubCategory);
        public SubCategory UpdateSubCategory(SubCategory SubCategory);
        public void DeleteSubCategory(SubCategory SubCategory);
    }
}
