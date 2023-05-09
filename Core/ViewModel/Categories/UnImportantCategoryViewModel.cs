using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel.Categories
{
    public class UnImportantCategoryViewModel
    {
        public int Id { get; set; }
        public string UnImportantCategoryName { get; set; }
        public SubCategoryViewModel SubCategoryViewModel { get; set; }
    }
}
