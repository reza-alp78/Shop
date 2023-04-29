using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Authentication
{
    public class LoginViewModel
    {
        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "ایمیل مقدار ندارد")]
        [EmailAddress(ErrorMessage = "ایمیل باید به صورت ایمیل باشد")]
        public string Email { get; set; }

        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "رمز عبور مقدار ندارد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Information=> Email: {Email}, Password: {Password}";
        }
    }
}
