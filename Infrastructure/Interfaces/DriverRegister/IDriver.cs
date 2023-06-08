using Core.Domain.Entity.DriverRegister;

namespace Infrastructure.Interfaces.DriverRegister
{
    public interface IDriver
    {
        public Task<List<Driver>> GetAllDrivers();
        public Task<Driver> GetDriverById(string id);
        public Task<Driver> AddDriver(Driver driver);
        public Driver UpdateDriver(Driver driver);
        public void DeleteDriver(Driver driver);
    }
}
