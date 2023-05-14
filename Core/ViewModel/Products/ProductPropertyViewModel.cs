using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel.Products
{
    public class ProductPropertyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public bool Name { get; set; } = true;
        [Display(Name = "قیمت اصلی")]
        public bool Price { get; set; } = true;
        [Display(Name = "قیمت با تخفیف")]
        public bool DiscountPrice { get; set; } = true;
        [Display(Name = "تخفیف")]
        public bool Discount { get; set; } = true;
        [Display(Name = "موجودی")]
        public bool Existance { get; set; }
        [Display(Name = "نمایش همیشه موجود است")]
        public bool IsAlwaysValid { get; set; }
        [Display(Name = "امتیاز")]
        public bool Rate { get; set; } = true;
        [Display(Name = "توضیحات")]
        public bool Description { get; set; } = true;
        [Display(Name = "رنگ")]
        public bool Color { get; set; }
        [Display(Name = "سایز")]
        public bool Size { get; set; }
        [Display(Name = "کشور")]
        public bool Country { get; set; }
        [Display(Name = "مدل")]
        public bool Model { get; set; }
        [Display(Name = "برند")]
        public bool Brand { get; set; }
        [Display(Name = "جنس")]
        public bool Gender { get; set; }
        [Display(Name = "وزن")]
        public bool Weight { get; set; }
        [Display(Name = "طول")]
        public bool Lenght { get; set; }
        [Display(Name = "عرض")]
        public bool Wide { get; set; }
        [Display(Name = "ارتفاع")]
        public bool Height { get; set; }
        [Display(Name = "گرافیک")]
        public bool Graphics { get; set; }
        [Display(Name = "پردازنده")]
        public bool Processor { get; set; }
        [Display(Name = "رم")]
        public bool RAM { get; set; }
        public Guid UserCreatorId { get; set; }

    }
}
