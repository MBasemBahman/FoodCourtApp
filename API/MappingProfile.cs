using Entities.DBModels.AppModels;
using Entities.DBModels.ShopModels;
using Entities.DtoModels.AppModels;
using Entities.DtoModels.ShopModels;

namespace Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapperConfiguration configuration = new(cfg =>
            {
                cfg.AllowNullCollections = false;
            });

            CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
            CreateMap<DateTime?, string>().ConvertUsing(new DateTimeNullableTypeConverter());
            CreateMap<string, string>().ConvertUsing(new StringTypeConverter());

            #region AppModels

            CreateMap<Branch, BranchDto>();

            CreateMap<BranchCreateOrEditDto, Branch>();

            CreateMap<Branch, BranchCreateOrEditDto>();
            #endregion

            #region ShopModels

             CreateMap<Shop, ShopDto>();

            CreateMap<ShopCreateDto, Shop>();

            CreateMap<ShopEditDto, Shop>();

             CreateMap<ShopGallery, ShopGalleryDto>();

             CreateMap<ShopGalleryCreateDto, ShopGallery>();

            CreateMap<ShopCreateOrEditDto, Shop>();

            CreateMap<Shop, ShopCreateOrEditDto>();
            #endregion
        }
    }

    public class DateTimeNullableTypeConverter : ITypeConverter<DateTime?, string>
    {
        public string Convert(DateTime? source, string destination, ResolutionContext context)
        {
            return source == null ? "" : source.Value.AddHours(2).ToShortDateTimeString();
        }
    }

    public class DateTimeTypeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.AddHours(2).ToShortDateTimeString();
        }
    }

    public class StringTypeConverter : ITypeConverter<string, string>
    {
        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                source = "";
            }
            else if (!string.IsNullOrWhiteSpace(source) && source.StartsWith("http"))
            {
                source = source.Replace(" ", "%20");
            }

            return source;
        }
    }
}
