using Entities.DBModels.ShopModels;
using Entities.DtoModels.ShopModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        [Route(nameof(GetShopGallerys))]
        public async Task<IEnumerable<ShopGalleryDto>> GetShopGallerys([FromQuery] ShopGalleryParameters parameters)
        {
            PagedList<ShopGalleryDto> shopGallerys = await _repositoryManager.ShopGallery.GetShopGallerysPaged(parameters);

            SetPagination(shopGallerys.MetaData, parameters);

            return shopGallerys;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<bool> Create([FromForm] ShopGalleryCreateDto model)
        {
            if (model.Fk_Shop <= 0 || model.Files == null || !model.Files.Any())
            {
                throw new Exception("Bad Request!");
            }

            ShopGallery shopGallery = _mapper.Map<ShopGallery>(model);

            foreach (IFormFile file in model.Files)
            {
                if (file != null)
                {
                    shopGallery.ImageUrl = await _repositoryManager.ShopGallery.UploadShopGalleryImages(_environment.WebRootPath, file);
                    shopGallery.StorageUrl = GetBaseUri();
                    _repositoryManager.ShopGallery.Create(shopGallery);
                }
            }

            await _repositoryManager.Save();

            return true;
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<bool> Delete([FromQuery, BindRequired] int id)
        {
            ShopGallery shopGallery = await _repositoryManager.ShopGallery.FindById(id, trackChanges: true);

            _repositoryManager.ShopGallery.Delete(shopGallery);
            await _repositoryManager.Save();

            return true;
        }
    }
}
