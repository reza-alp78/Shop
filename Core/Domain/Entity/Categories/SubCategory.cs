namespace Core.Domain.Entity.Categories
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public Category category { get; set; }
        public List<UnImportantCategory> UnImportantCategory { get; set; }
    }
}
