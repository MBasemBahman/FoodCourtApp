using Entities.DBModels.ShopModels;
using Entities.DtoModels.ShopModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        [HttpGet]
        [Route(nameof(GetShopById))]
        public ShopDto GetShopById([FromQuery, BindRequired] int id)
        {
            return _repositoryManager.Shop.GetShopbyId(id);
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ShopDto> Create([FromBody] ShopCreateDto model)
        {
            AuthenticatedDto auth = (AuthenticatedDto)Request.HttpContext.Items[ApiConstants.Authenticated];


            Shop shop = _mapper.Map<Shop>(model);

            shop.CreatedBy = auth.Name;

            if (model.ShopImageFile != null)
            {
                shop.ImageUrl = await _repositoryManager.Shop.UploadShopImage(_environment.WebRootPath, model.ShopImageFile);
                shop.StorageUrl = GetBaseUri();
            }
            _repositoryManager.Shop.Create(shop);
            await _repositoryManager.Save();

            return _repositoryManager.Shop.GetShopbyId(shop.Id);
        }

        [HttpPut]
        [Route(nameof(Edit))]
        public async Task<ShopDto> Edit(
            [FromQuery, BindRequired] int id,
            [FromBody] ShopEditDto model)
        {
            AuthenticatedDto auth = (AuthenticatedDto)Request.HttpContext.Items[ApiConstants.Authenticated];

            Shop shop = await _repositoryManager.Shop.FindById(id, trackChanges: true);

            _mapper.Map(model, shop);

            shop.LastModifiedBy = auth.Name;

            if (model.ShopImageFile != null)
            {
                shop.ImageUrl = await _repositoryManager.Shop.UploadShopImage(_environment.WebRootPath, model.ShopImageFile);
                shop.StorageUrl = GetBaseUri();
            }

            await _repositoryManager.Save();

            return _repositoryManager.Shop.GetShopbyId(shop.Id);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<bool> Delete([FromQuery, BindRequired] int id)
        {
            Shop shop = await _repositoryManager.Shop.FindById(id, trackChanges: true);

            _repositoryManager.Shop.Delete(shop);
            await _repositoryManager.Save();

            return true;
        }
    }
}
