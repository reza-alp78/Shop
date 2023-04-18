using Microsoft.AspNetCore.Identity;

namespace Core.IdentityEntity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PersonName { get; set; }
    }
}
