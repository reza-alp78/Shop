using Core.ViewModel.Categories;
using Core.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.CategoriesAndProducts
{
    public class CategoryProductPropertyViewModel
    {
        public int Id { get; set; }
        public int WhichCategoryId { get; set; }
        public WhichCategoryViewModel WhichCategoryViewModel { get; set; }
        public int ProductPropertyId { get; set; }
        public ProductPropertyViewModel ProductPropertyViewModel { get; set; }
    }
}
