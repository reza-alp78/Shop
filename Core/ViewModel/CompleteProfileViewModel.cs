using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class CompleteProfileViewModel
    {
        [Required]
        public Guid Id { get; set; }

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

        public override string ToString()
        {
            return $"Information=> Id: {Id}, Province: {Province}, City: {City}, Adress: {Adress}, ZipCode: {ZipCode}, Description: {Description}";
        }

    }
}
