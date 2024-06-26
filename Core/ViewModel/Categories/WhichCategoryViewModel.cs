﻿using Core.ViewModel.Products;

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
    }
}
