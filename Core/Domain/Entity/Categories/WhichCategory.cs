using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.Products;

namespace Core.Domain.Entity.Categories
{
    public class WhichCategory
    {
        public int Id { get; set; }
        public int MainCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int UnImportantCategoryId { get; set; }

        public List<Product> Products { get; set; }
        public List<CategoryProductProperty> CategoryProductProperties { get; set; }
    }
}
