using Core.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.DriverRegister
{
    public class DriverViewMolde
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
    }
}
