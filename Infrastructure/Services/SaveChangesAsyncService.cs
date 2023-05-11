using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces;

namespace Infrastructure.Services
{
    public class SaveChangesAsyncService : ISaveChangesAsync
    {
        private readonly ShopDbContext _db;

        public SaveChangesAsyncService(ShopDbContext shopDbContext)
        {
            _db = shopDbContext;
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
