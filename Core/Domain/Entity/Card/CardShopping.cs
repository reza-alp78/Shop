namespace Core.Domain.Entity.Card
{
    public class CardShopping
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }

    }
}
