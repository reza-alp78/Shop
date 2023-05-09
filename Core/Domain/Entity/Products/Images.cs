namespace Core.Domain.Entity.Products
{
    public class Images
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte[] ImageProduct { get; set; }

        public List<Product> products { get; set; }
    }
}
