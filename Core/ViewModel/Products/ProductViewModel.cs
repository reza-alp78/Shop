using Core.ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Products
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public double Discount { get; set; }
        public int Existance { get; set; }
        public bool IsAlwaysValid { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Lenght { get; set; }
        public string Wide { get; set; }
        public string Height { get; set; }
        public string Graphics { get; set; }
        public string Processor { get; set; }
        public string RAM { get; set; }
        public bool IsExist { get; set; }
        public Guid CreatorId { get; set; }

        public WhichCategoryViewModel WhichCategoryViewModel { get; set; }
    }
}
