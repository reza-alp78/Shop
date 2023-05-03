using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Authentication
{
    public class CompleteProfileViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [DisplayName("نام")]
        [Required(ErrorMessage = "نام مقدار ندارد")]
        public string Name { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگی مقدار ندارد")]
        public string LastName { get; set; }


        public string FullName => Name + " " + LastName;


        [DisplayName("شماره تماس")]
        [Required(ErrorMessage = "شماره تماس مقدار ندارد")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس باید به صورت عدد باشد")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "باید شماره تماس وارد نمایید")]
        public string PhoneNumbe { get; set; }

        [DisplayName("استان")]
        [Required(ErrorMessage = "استان مقدار ندارد")]
        public string Province { get; set; }

        [DisplayName("شهر")]
        [Required(ErrorMessage = "شهر مقدار ندارد")]
        public string City { get; set; }

        [DisplayName("آدرس")]
        [Required(ErrorMessage = "آدرس مقدار ندارد")]
        public string Adress { get; set; }

        [DisplayName("کدپستی")]
        [Required(ErrorMessage = "کدپستی مقدار ندارد")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "کدپستی عددی 10 رقمی میباشد")]
        public int ZipCode { get; set; }

        [DisplayName("توضیحات")]
        [StringLength(120, ErrorMessage = "حداکثر کاراکتر مجاز 120 میباشد")]
        public string Description { get; set; }

    }
}
