using Core.ViewModel.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Core.ViewModel.DriverRegister
{
    public class DriverViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "نام مقدار ندارد")]
        public string Name { get; set; }

        [DisplayName("کد ملی")]
        [Required(ErrorMessage = "کد ملی مقدار ندارد")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "کد ملی عددی 10 رقمی میباشد")]
        public long NationalCode { get; set; }

        [DisplayName("شماره تماس")]
        [Required(ErrorMessage = "شماره تماس مقدار ندارد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس باید به صورت عدد باشد")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "باید شماره تماس وارد نمایید")]
        public string PhoneNumber { get; set; }

        [DisplayName("آدرس")]
        [Required(ErrorMessage = "آدرس مقدار ندارد")]
        public string Address { get; set; }

    }
}
