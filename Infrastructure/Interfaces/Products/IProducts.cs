using Core.Domain.Entity.Products;

namespace Infrastructure.Interfaces.Products
{
    public interface IProducts
    {
        public Task<Product> AddProduct(Product product);
        public Task<List<Product>> GetAllProductsByWhichCategoryId(int whichCategoryId);
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(long productId);
        public Task<List<Product>> GetAllProductsByWhichCategoryIdAndByUserCretorId(int whichCategoryId, Guid userCreatorId);
        public Product UpdateProduct(Product product);
        public void DeleteProduct(Product product);
    }
}
