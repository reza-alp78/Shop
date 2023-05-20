namespace Core.Domain.Entity.Products
{
    public class Images
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product product { get; set; }
        public byte[] ImageProduct { get; set; }
        
    }
}
