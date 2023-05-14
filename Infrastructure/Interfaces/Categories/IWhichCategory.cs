using Core.Domain.Entity.Categories;

namespace Infrastructure.Interfaces.Categories
{
    public interface IWhichCategory
    {
        public Task<WhichCategory> AddWhichCategory(WhichCategory whichCategory);
        public Task<WhichCategory> GetWhichCategoryByIds(int mainCategoryId, int categoryId, int subcategoryId);
        public Task<WhichCategory> GetWhichCategoryByIds(int mainCategoryId, int categoryId, int subcategoryId, int unImportantCategoryId);
        public WhichCategory UpdateWhichCategory(WhichCategory whichCategory);
        public void DeleteWhichCategory(WhichCategory whichCategory);
    }
}
