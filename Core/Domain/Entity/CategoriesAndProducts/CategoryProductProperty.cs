namespace Core.Domain.Entity.CategoriesAndProducts
{
    public class CategoryProductProperty
    {
        public int Id { get; set; }
        public int WhichCategoryId { get; set; }      
        public int ProductPropertyId { get; set; }      
    }
}
