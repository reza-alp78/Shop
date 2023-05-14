using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.Products;
using Core.ViewModel.Categories;
using Core.ViewModel.CategoriesAndProducts;
using Core.ViewModel.Products;

namespace Ui.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Categories

            CreateMap<MainCategoryViewModel, MainCategory>().ReverseMap();

            CreateMap<CategoryViewModel, Category>().ReverseMap();

            CreateMap<SubCategoryViewModel, SubCategory>().ReverseMap();

            CreateMap<UnImportantCategoryViewModel, UnImportantCategory>().ReverseMap();

            #endregion

            #region Products

            CreateMap<ProductPropertyViewModel, ProductProperty>().ReverseMap();

            CreateMap<WhichCategoryViewModel, WhichCategory>().ReverseMap();

            #endregion

            #region CategoriesAndProducts

            CreateMap<CategoryProductPropertyViewModel, CategoryProductProperty>().ReverseMap();

            #endregion

        }
    }
}
