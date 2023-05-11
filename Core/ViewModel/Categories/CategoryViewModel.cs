using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "دسته")]
        [Required(ErrorMessage = "دسته نمیتواند خالی باشد")]
        public string CategoryName { get; set; }
        public int MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }

        public MainCategoryViewModel MainCategoryViewModel { get; set; }
        public List<SubCategoryViewModel> subCategoryViewModels { get; set; }
    }
}
