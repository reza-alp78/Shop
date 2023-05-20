using Core.Domain.Entity.Products;

namespace Infrastructure.Interfaces.Products
{
    public interface IPoducts
    {
        public Task<Product> AddProduct(Product product);
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int productId);
        public Task<List<Product>> GetProductsByUserCretorId(Guid userCreatorId);
        public Product UpdateProduct(Product product);
        public void DeleteProduct(Product product);
    }
}
