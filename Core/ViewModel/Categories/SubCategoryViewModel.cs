using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Categories
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public CategoryViewModel categoryViewModel { get; set; }
        public List<UnImportantCategoryViewModel> UnImportantCategoryViewModels { get; set; }
    }
}
