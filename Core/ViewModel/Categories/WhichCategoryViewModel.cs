using Core.ViewModel.CategoriesAndProducts;
using Core.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Categories
{
    public class WhichCategoryViewModel
    {
        public int Id { get; set; }
        public int MainCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int UnImportantCategoryId { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
        public CategoryProductPropertyViewModel CategoryProductPropertyViewModel { get; set; }
    }
}
