using Core.Domain.Entity.Categories;
using Core.Domain.Entity.Products;

namespace Infrastructure.Interfaces.Products
{
    public interface IProductProperty
    {
        public Task<ProductProperty> AddProductProperty(ProductProperty productProperty);
        public Task<ProductProperty> GetProductPropertyByProductPropertyId(int productPropertyId);
        public ProductProperty UpdateProductProperty(ProductProperty productProperty);
        public void DeleteProductProperty(ProductProperty productProperty);
    }
}
