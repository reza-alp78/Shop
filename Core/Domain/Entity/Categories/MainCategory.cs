using Core.Domain.Entity.Products;

namespace Core.Domain.Entity.Categories
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string MainCategoryName { get; set; }
        public List<Category> Category { get; set; }
    }
}
