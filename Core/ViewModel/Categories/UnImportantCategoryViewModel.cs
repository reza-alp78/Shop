using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.Categories
{
    public class UnImportantCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "دسته فرعی")]
        [Required(ErrorMessage = "دسته فرعی نمیتواند خالی باشد")]
        public string UnImportantCategoryName { get; set; }
        public int SubCategoryId { get; set; }       
        public string SubCategoryName { get; set; }
        public SubCategoryViewModel SubCategoryViewModel { get; set; }
    }
}
