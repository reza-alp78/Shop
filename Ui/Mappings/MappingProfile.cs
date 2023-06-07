using AutoMapper;
using Core.Domain.Entity.Categories;
using Core.Domain.Entity.CategoriesAndProducts;
using Core.Domain.Entity.DriverRegister;
using Core.Domain.Entity.Products;
using Core.ViewModel.Categories;
using Core.ViewModel.CategoriesAndProducts;
using Core.ViewModel.DriverRegister;
using Core.ViewModel.Products;
using Ui.HandShort;

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

            CreateMap<WhichCategoryViewModel, WhichCategory>().ReverseMap();

            #endregion

            #region CategoriesAndProducts

            CreateMap<CategoryProductPropertyViewModel, CategoryProductProperty>().ReverseMap();

            CreateMap<ProductRegisterViewModel, ProductRegister>().ReverseMap();

            #endregion

            #region DriverRegister

            CreateMap<DriverViewModel, Driver>().ReverseMap();

            #endregion

            #region Products

            CreateMap<BuyViewModel, Buy>().ReverseMap();

            CreateMap<ImagesViewModel, Images>().ReverseMap();

            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<ProductPropertyViewModel, ProductProperty>().ReverseMap();

            CreateMap<ProductPropertyViewModel, Product>().AfterMap((src, dest) =>
            {
                dest.Name = src.NameProduct;
                dest.Price = src.PriceProduct;
                dest.DiscountPrice = src.DiscountPriceProduct;
                dest.Discount = Extension.Discount(dest.Price, dest.DiscountPrice);
                dest.Existance = src.ExistanceProduct;
                dest.IsAlwaysValid = src.IsAlwaysValid;
                dest.Description = src.DescriptionProduct;
                dest.Rate = null;
                dest.Color = src.ColorProduct;
                dest.Size = src.SizeProduct;
                dest.Country = src.CountryProduct;
                dest.Model = src.ModelProduct;
                dest.Brand = src.BrandProduct;
                dest.Gender = src.GenderProduct;
                dest.Weight = src.WeightProduct;
                dest.Lenght = src.LenghtProduct;
                dest.Wide = src.WideProduct;
                dest.Height = src.HeightProduct;
                dest.Graphics = src.GraphicsProduct;
                dest.Processor = src.ProcessorProduct;
                dest.RAM = src.RAMProduct;
                dest.UserCreatorId = src.UserCreatorId;
            });

            #endregion           

        }
    }
}
