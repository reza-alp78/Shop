namespace Core.Domain.Entity.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public MainCategory MainCategory { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
