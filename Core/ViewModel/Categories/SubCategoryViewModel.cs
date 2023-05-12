using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.Categories
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "زیر دسته")]
        [Required(ErrorMessage = "زیر دسته نمیتواند خالی باشد")]
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CategoryViewModel categoryViewModel { get; set; }
        public List<UnImportantCategoryViewModel> UnImportantCategoryViewModels { get; set; }
    }
}
