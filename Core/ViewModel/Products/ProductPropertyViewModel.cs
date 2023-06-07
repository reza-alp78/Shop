using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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

        public string NameProduct { get; set; }
        public double PriceProduct { get; set; }
        public double DiscountPriceProduct { get; set; }
        public double DiscountProduct { get; set; }
        public string ExistanceProduct { get; set; }
        public double RateProduct { get; set; }
        public string DescriptionProduct { get; set; }
        public string ColorProduct { get; set; }
        public string SizeProduct { get; set; }
        public string CountryProduct { get; set; }
        public string ModelProduct { get; set; }
        public string BrandProduct { get; set; }
        public string GenderProduct { get; set; }
        public string WeightProduct { get; set; }
        public string LenghtProduct { get; set; }
        public string WideProduct { get; set; }
        public string HeightProduct { get; set; }
        public string GraphicsProduct { get; set; }
        public string ProcessorProduct { get; set; }
        public string RAMProduct { get; set; }

        public Guid UserCreatorId { get; set; }
        public List<byte[]> Images { get; set; }

    }
}
