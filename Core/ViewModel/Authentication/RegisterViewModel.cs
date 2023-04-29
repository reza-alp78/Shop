using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Authentication
{
    public class RegisterViewModel
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = "نام مقدار ندارد")]
        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگی مقدار ندارد")]
        public string LastName { get; set; }


        public string FullName => Name + " " + LastName;


        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "ایمیل مقدار ندارد")]
        [EmailAddress(ErrorMessage = "ایمیل باید به صورت ایمیل باشد")]
        public string Email { get; set; }


        [DisplayName("شماره تماس")]
        [Required(ErrorMessage = "شماره تماس مقدار ندارد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس باید به صورت عدد باشد")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "باید شماره تماس وارد نمایید")]
        public string Phone { get; set; }


        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "رمز عبور مقدار ندارد")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{6,24}$", ErrorMessage = "رمز عبور بایداز ۶ تا ۲۴ کاراکتر باشد ٫ که حداقل شامل یک عدد ٫ یک حرف کوچک ٫ یک حرف بزرگ و یک کاراکتر خاص مانند (!@#$&*) باشد")]
        public string Password { get; set; }


        [DisplayName("تایید رمز عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور ها یکی نمیباشند")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"Information=> FullName: {FullName}, Email: {Email}, Phone: {Phone}, Password: {Password}, ConfrimPassword: {ConfirmPassword}";
        }
    }
}
