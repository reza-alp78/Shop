namespace Core.Domain.Entity.Categories
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<UnImportantCategory> UnImportantCategory { get; set; }
    }
}
