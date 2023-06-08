using Core.Domain.Entity.DriverRegister;
using Dapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Interfaces.DriverRegister;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DriverRegister
{
    public class DriverService : IDriver
    {

        #region constructor

        private readonly ShopDbContext _db;
        private readonly DapperContext _dapper;

        public DriverService(ShopDbContext shopDbContext, DapperContext context)
        {
            _db = shopDbContext;
            _dapper = context;
        }

        #endregion

        public async Task<List<Driver>> GetAllDrivers()
        {
            var query = "SELECT * FROM Drivers";
            using (var connection = _dapper.CreateConnection())
            {
                var drivers = await connection.QueryAsync<Driver>(query);
                return drivers.ToList();
            }
        }

        public async Task<Driver> GetDriverById(string id)
        {
            var query = $"SELECT * FROM Drivers WHERE Id = {id}";
            using (var connection = _dapper.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Driver>(query);
            }
        }

        public async Task<Driver> AddDriver(Driver driver)
        {
            await _db.Drivers.AddAsync(driver);
            return driver;
        }

        public Driver UpdateDriver(Driver driver)
        {
            _db.Entry(driver).State = EntityState.Modified;
            return driver;
        }

        public void DeleteDriver(Driver driver)
        {
            _db.Entry(driver).State = EntityState.Deleted;
        }

    }
}
