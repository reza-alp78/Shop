using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.Card
{
    public class HistoryCardShoppingViewModel
    {
        public long Id { get; set; }

        [Display(Name = "نام کاربر")]
        public Guid UserId { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "محصول نمیتواند خالی باشد")]
        public long ProductId { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "تعداد نمیتواند خالی باشد")]
        public int count { get; set; }
    }
}
