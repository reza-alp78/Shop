namespace Core.ViewModel.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int MainCategoryId { get; set; }
        public MainCategoryViewModel MainCategoryViewModel { get; set; }
        public List<SubCategoryViewModel> subCategoryViewModels { get; set; }
    }
}
