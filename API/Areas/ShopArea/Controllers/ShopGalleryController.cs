using Entities.DtoModels.ShopModels;

namespace API.Areas.ShopArea.Controllers
{
    [ApiVersion("1.0")]
    [Area("Shop")]
    [ApiExplorerSettings(GroupName = "Shop")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    public class ShopGalleryController : ExtendControllerBase
    {
        public ShopGalleryController(
        IMapper mapper,
        RepositoryManager repositoryManager,
        LinkGenerator linkGenerator,
        IWebHostEnvironment environment) : base(mapper, repositoryManager, linkGenerator, environment)
        { }

        [HttpGet]
        [Route(nameof(GetShopGalleries))]
        public async Task<IEnumerable<ShopGalleryDto>> GetShopGalleries([FromQuery] ShopGalleryParameters parameters)
        {
            IEnumerable<ShopGalleryDto> shopGallerys = await _repositoryManager.ShopGallery.GetShopGallerys(parameters).ToListAsync();

            return shopGallerys;
        }
    }
}
