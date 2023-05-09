using Core.ViewModel.Products;
using Microsoft.AspNetCore.Identity;

namespace Core.IdentityEntity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public int ZipCode { get; set; }
        public string Description { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
    }
}
