using Entities.DtoModels.ShopModels;

namespace API.Areas.ShopArea.Controllers
{
    [ApiVersion("1.0")]
    [Area("Shop")]
    [ApiExplorerSettings(GroupName = "Shop")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    public class ShopController : ExtendControllerBase
    {
        public ShopController(
        IMapper mapper,
        RepositoryManager repositoryManager,
        LinkGenerator linkGenerator,
        IWebHostEnvironment environment) : base(mapper, repositoryManager, linkGenerator, environment)
        { }

        [HttpGet]
        [Route(nameof(GetShops))]
        public async Task<IEnumerable<ShopDto>> GetShops([FromQuery] ShopParameters parameters)
        {
            PagedList<ShopDto> shops = await _repositoryManager.Shop.GetShopsPaged(parameters);

            SetPagination(shops.MetaData, parameters);

            return shops;
        }
    }
}
