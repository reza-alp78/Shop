using Core.Domain.Entity.Categories;
using Core.Domain.Entity.CategoriesAndProducts;

namespace Core.Domain.Entity.Products
{
    public class ProductProperty
    {
        public int Id { get; set; }
        public bool Name { get; set; }
        public bool Price { get; set; }
        public bool DiscountPrice { get; set; }
        public bool Discount { get; set; }
        public bool Existance { get; set; }
        public bool IsAlwaysValid { get; set; }
        public bool Rate { get; set; }
        public bool Description { get; set; }
        public bool Color { get; set; }
        public bool Size { get; set; }
        public bool Country { get; set; }
        public bool Model { get; set; }
        public bool Brand { get; set; }
        public bool Gender { get; set; }
        public bool Weight { get; set; }
        public bool Lenght { get; set; }
        public bool Wide { get; set; }
        public bool Height { get; set; }
        public bool Graphics { get; set; }
        public bool Processor { get; set; }
        public bool RAM { get; set; }
        public Guid UserCreatorId { get; set; }
        public List<CategoryProductProperty> CategoryProductProperties { get; set; }      

    }
}
