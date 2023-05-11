namespace Core.Domain.Entity.Categories
{
    public class UnImportantCategory
    {
        public int Id { get; set; }
        public string UnImportantCategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
