using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public MainCategoryViewModel MainCategoryViewModel { get; set; }
        public List<SubCategoryViewModel> subCategoryViewModels { get; set; }
    }
}
