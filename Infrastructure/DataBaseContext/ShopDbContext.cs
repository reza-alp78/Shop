using Core.IdentityEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBaseContext
{
    public class ShopDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {         
        }

    }
}
