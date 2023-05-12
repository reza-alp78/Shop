using Core.Domain.Entity.Categories;

namespace Infrastructure.Interfaces.Categories
{
    public interface IUnImportantCategory
    {
        public Task<List<UnImportantCategory>> GetAllUnImportantCategories();
        public Task<List<UnImportantCategory>> GetAllUnImportantCategoriesBySubCategoriesId(int subCategoryId);
        public Task<UnImportantCategory> GetUnImportantCategory(int id);
        public Task<UnImportantCategory> AddUnImportantCategory(UnImportantCategory unImportantCategory);
        public UnImportantCategory UpdateUnImportantCategory(UnImportantCategory unImportantCategory);
        public void DeleteUnImportantCategory(UnImportantCategory unImportantCategory);
    }
}
