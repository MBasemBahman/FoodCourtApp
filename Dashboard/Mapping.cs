using Entities.DBModels.AppModels;
using Entities.DBModels.ShopModels;
using Entities.DtoModels.AppModels;
using Entities.DtoModels.ShopModels;
using Dashboard.ViewModel;

namespace Dashboard
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            MapperConfiguration configuration = new(cfg =>
            {
                cfg.AllowNullCollections = false;
            });



            CreateMap<DateTime, string>().ConvertUsing(new DateTimeTypeConverter());
            CreateMap<DateTime?, string>().ConvertUsing(new DateTimeNullableTypeConverter());
            CreateMap<string, string>().ConvertUsing(new StringTypeConverter());


            CreateMap<DtParameters, RequestParameters>()
           .ForMember(dest => dest.SearchTerm, opt => opt.MapFrom(src => src.Search == null ? "" : src.Search.Value))
           .ForMember(dest => dest.OrderBy, opt =>
                              opt.MapFrom(src => src.Order == null ?
                                                 "" :
                                                 (src.Order[0].Dir.ToString().ToLower() == "asc" ?
                                                  src.Columns[src.Order[0].Column].Data :
                                                  (src.Columns[src.Order[0].Column].Data.Contains(',') ?
                                                   src.Columns[src.Order[0].Column].Data.Replace(",", " desc,") :
                                                   src.Columns[src.Order[0].Column].Data + " desc"))))
           .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.Length))
           .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => (src.Start / src.Length) + 1))
           .IncludeAllDerived();

            #region AppModels


            CreateMap<BranchCreateOrEditDto, Branch>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Branch, BranchCreateOrEditDto>()
                                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.StorageUrl + src.ImageUrl));
          

            #endregion

            #region ShopModels


            CreateMap<ShopCreateOrEditDto, Shop>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Shop, ShopCreateOrEditDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.StorageUrl + src.ImageUrl));

            CreateMap<ShopFilter, ShopParameters>();


            #endregion


            #region Auth Models


            CreateMap<SystemUserCreateOrEditDto, SystemUser>();

            CreateMap<SystemUser, SystemUserCreateOrEditDto>();

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
