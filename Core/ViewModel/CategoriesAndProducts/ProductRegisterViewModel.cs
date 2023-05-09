using Core.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.CategoriesAndProducts
{
    public class ProductRegisterViewModel
    {
        public long Id { get; set; }
        public int WhichCategoryId { get; set; }
        public long ProductId { get; set; }

        public List<BuyViewModel> BuyViewModels { get; set; }
    }
}
