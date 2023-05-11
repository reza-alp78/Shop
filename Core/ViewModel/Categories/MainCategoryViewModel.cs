using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.Categories
{
    public class MainCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "دسته اصلی")]
        [Required(ErrorMessage ="دسته اصلی نمیتواند خالی باشد")]
        public string MainCategoryName { get; set; }
        public List<CategoryViewModel> CategoryViewModel { get; set; }
    }
}
