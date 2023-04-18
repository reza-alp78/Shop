using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class RegisterViewModel
    {
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "نام کاربری مقدار ندارد")]
        [StringLength(20,ErrorMessage ="حداکثر کاراکتر مجاز ۲۰ میباشد")]
        public string UserName { get; set; }


        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "ایمیل مقدار ندارد")]
        [EmailAddress(ErrorMessage = "ایمیل باید به صورت ایمیل باشد")]
        public string Email { get; set; }


        [DisplayName("شماره تماس")]
        [Required(ErrorMessage = "شماره تماس مقدار ندارد")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="شماره تماس باید به صورت عدد باشد")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="باید شماره تماس وارد نمایید")]
        public string Phone { get; set; }


        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "رمز عبور مقدار ندارد")]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="حداقل کاراکتر مجاز ۸ میباشد")]
        [MaxLength(24,ErrorMessage ="حداکثر کاراکتر مجاز ۲۴ میباشد")]
        public string Password { get; set; }


        [DisplayName("تایید رمز عبور")]
        [Compare("Password",ErrorMessage ="رمز عبور ها یکی نمیباشند")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"Information=> UserName: {UserName}, Email: {Email}, Phone: {Phone}, Password: {Password}, ConfrimPassword: {ConfirmPassword}";
        }
    }
}
