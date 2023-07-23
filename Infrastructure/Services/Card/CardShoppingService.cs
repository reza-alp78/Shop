using Core.Domain.Entity.Card;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Card;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Card
{
    public class CardShoppingService : ICardShopping
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public CardShoppingService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<CardShopping>> GetCardShoppingByUserId(Guid userId)
        {
            var query = $"SELECT * FROM CardShoppings WHERE UserId = '{userId}'";
            using (var connection = _dapper.CreateConnection())
            {
                var products = await connection.QueryAsync<CardShopping>(query);
                return products.ToList();
            }
        }

        public async Task<CardShopping> GetCardShoppingByUserIdAndProductId(Guid userId, long productId)
        {
            var query = $"SELECT * FROM CardShoppings WHERE UserId = '{userId}' AND ProductId = {productId}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<CardShopping>(query);
            }
        }

        public async Task<CardShopping> AddCardShopping(CardShopping cardShopping)
        {
            await _db.CardShoppings.AddAsync(cardShopping);
            return cardShopping;
        }

        public CardShopping UpdateCardShopping(CardShopping cardShopping)
        {
            _db.Entry(cardShopping).State = EntityState.Modified;
            return cardShopping;
        }

        public void DeleteCardShopping(CardShopping cardShopping)
        {
            _db.Entry(cardShopping).State = EntityState.Deleted;
        }

    }
}
