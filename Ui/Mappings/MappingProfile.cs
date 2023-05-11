using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.ViewModel.Categories;

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

        }
    }
}
