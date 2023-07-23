using Core.Domain.Entity.Card;

namespace Infrastructure.Interfaces.Card
{
    public interface ICardShopping
    {
        public Task<List<CardShopping>> GetCardShoppingByUserId(Guid userId);
        public Task<CardShopping> GetCardShoppingByUserIdAndProductId(Guid userId, long productId);
        public Task<CardShopping> AddCardShopping(CardShopping cardShopping);
        public CardShopping UpdateCardShopping(CardShopping cardShopping);
        public void DeleteCardShopping(CardShopping cardShopping);
    }
}
