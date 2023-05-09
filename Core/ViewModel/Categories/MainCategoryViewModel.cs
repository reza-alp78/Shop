using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Categories
{
    public class MainCategoryViewModel
    {
        public int Id { get; set; }
        public string MainCategoryName { get; set; }
        public List<CategoryViewModel> CategoryViewModel { get; set; }
    }
}
