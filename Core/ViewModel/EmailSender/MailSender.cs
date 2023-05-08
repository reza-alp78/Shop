using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.EmailSender
{
    public class MailSender
    {

        [DisplayName("تایید کد")]
        [Required(ErrorMessage ="تایید کد نمیتواند خالی باشد")]
        public string SendCode { get; set; }

        [DisplayName("ایمیل")]
        public string Email { get; set; }

        
        [DisplayName("رمز عبور")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{6,24}$", ErrorMessage = "رمز عبور بایداز ۶ تا ۲۴ کاراکتر باشد ٫ که حداقل شامل یک عدد ٫ یک حرف کوچک ٫ یک حرف بزرگ و یک کاراکتر خاص مانند (!@#$&*) باشد")]
        public string Password { get; set; }

        [DisplayName("تایید رمز عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور ها یکی نمیباشند")]
        public string ConfirmPassword { get; set; }

    }
}
