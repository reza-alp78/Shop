using Core.Domain.Entity.Card;

namespace Infrastructure.Interfaces.Card
{
    public interface IHistoryCardShopping
    {
        public Task<List<HistoryCardShopping>> GetHistoryCardShoppingByUserId(Guid userId);
        public Task<HistoryCardShopping> AddHistoryCardShopping(HistoryCardShopping historycardShopping);
        public HistoryCardShopping UpdateHistoryCardShopping(HistoryCardShopping historycardShopping);
        public void DeleteHistoryCardShopping(HistoryCardShopping historycardShopping);
    }
}
