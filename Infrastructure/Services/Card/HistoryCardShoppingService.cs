using Core.Domain.Entity.Card;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.Card;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Card
{
    public class HistoryCardShoppingService : IHistoryCardShopping
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public HistoryCardShoppingService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<HistoryCardShopping>> GetHistoryCardShoppingByUserId(Guid userId)
        {
            var query = $"SELECT * FROM CardShoppings WHERE UserId = '{userId}'";
            using (var connection = _dapper.CreateConnection())
            {
                var products = await connection.QueryAsync<HistoryCardShopping>(query);
                return products.ToList();
            }
        }

        public async Task<HistoryCardShopping> AddHistoryCardShopping(HistoryCardShopping historyCardShopping)
        {
            await _db.HistoryCardShoppings.AddAsync(historyCardShopping);
            return historyCardShopping;
        }

        public HistoryCardShopping UpdateHistoryCardShopping(HistoryCardShopping historyCardShopping)
        {
            _db.Entry(historyCardShopping).State = EntityState.Modified;
            return historyCardShopping;
        }

        public void DeleteHistoryCardShopping(HistoryCardShopping historyCardShopping)
        {
            _db.Entry(historyCardShopping).State = EntityState.Deleted;
        }

    }
}
