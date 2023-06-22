using Core.ViewModel.Categories;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Core.ViewModel.Products
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name = "قیمت اصلی")]
        public double Price { get; set; }
        [Display(Name = "قیمت با تخفیف")]
        public double DiscountPrice { get; set; }
        [Display(Name = "تخفیف")]
        public double Discount { get; set; }
        [Display(Name = "موجودی")]
        public string Existance { get; set; }
        [Display(Name = "نمایش همیشه موجود است")]
        public bool IsAlwaysValid { get; set; }
        [Display(Name = "امتیاز")]
        public string Rate { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "رنگ")]
        public string Color { get; set; }
        [Display(Name = "سایز")]
        public string Size { get; set; }
        [Display(Name = "کشور")]
        public string Country { get; set; }
        [Display(Name = "مدل")]
        public string Model { get; set; }
        [Display(Name = "برند")]
        public string Brand { get; set; }
        [Display(Name = "جنس")]
        public string Gender { get; set; }
        [Display(Name = "وزن")]
        public string Weight { get; set; }
        [Display(Name = "طول")]
        public string Lenght { get; set; }
        [Display(Name = "عرض")]
        public string Wide { get; set; }
        [Display(Name = "ارتفاع")]
        public string Height { get; set; }
        [Display(Name = "گرافیک")]
        public string Graphics { get; set; }
        [Display(Name = "پردازنده")]
        public string Processor { get; set; }
        [Display(Name = "رم")]
        public string RAM { get; set; }

        public Guid UserCreatorId { get; set; }
        public List<byte[]> Images { get; set; }
        public string ImageString { get; set; }
        public List<string> ImagesStrings { get; set; } = new List<string>();
        
        public WhichCategoryViewModel WhichCategoryViewModel { get; set; }
    }
}
