namespace Core.Domain.Entity.Products
{
    public class ProductRateAndComment
    {
        public long Id { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }

    }
}
