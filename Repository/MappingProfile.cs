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

            _ = CreateMap<Branch, BranchDto>();

            _ = CreateMap<BranchCreateOrEditDto, Branch>();

            #endregion

            #region ShopModels

            _ = CreateMap<Shop, ShopDto>();

            _ = CreateMap<ShopCreateDto, Shop>();

            _ = CreateMap<ShopEditDto, Shop>();

            _ = CreateMap<ShopGallery, ShopGalleryDto>();

            _ = CreateMap<ShopGalleryCreateDto, ShopGallery>();

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
