using Core.Domain.Entity.Categories;
using Core.Domain.Entity.Products;

namespace Core.Domain.Entity.CategoriesAndProducts
{
    public class CategoryProductProperty
    {
        public int Id { get; set; }
        public int WhichCategoryId { get; set; }
        public WhichCategory WhichCategory { get; set; }
        public int ProductPropertyId { get; set; }
        public ProductProperty ProductProperty { get; set; }
    }
}
