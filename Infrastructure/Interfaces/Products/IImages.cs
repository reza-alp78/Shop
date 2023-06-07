using Core.Domain.Entity.Products;

namespace Infrastructure.Interfaces.Products
{
    public interface IImages
    {
        public Task<Images> AddImages(Images images);
        public Task<List<Images>> GetAllImages();
        public Task<List<Images>> GetImagesByProductId(long productId);
        public Images UpdateImages(Images images);
        public void DeleteImages(Images images);
    }
}
