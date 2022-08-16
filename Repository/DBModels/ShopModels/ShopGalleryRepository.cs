using Entities.DBModels.ShopModels;
using Entities.DtoModels.ShopModels;
using Microsoft.AspNetCore.Http;
using Services;

namespace Repository.DBModels.ShopModels
{
    public class ShopGalleryRepository : RepositoryBase<ShopGallery>
    {

        public ShopGalleryRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<ShopGalleryDto> GetShopGallerys(ShopGalleryParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new ShopGalleryDto
                   {
                       Id = a.Id,
                       ImageUrl = a.StorageUrl + a.ImageUrl,
                       CreatedAtVal = a.CreatedAt,
                       Order = a.Order
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
        }

        public async Task<PagedList<ShopGalleryDto>> GetShopGallerysPaged(
          ShopGalleryParameters parameters)
        {
            return await PagedList<ShopGalleryDto>.ToPagedList(GetShopGallerys(parameters), parameters.PageNumber, parameters.PageSize);
        }

        public ShopGalleryDto GetShopGallerybyId(int id)
        {
            return GetShopGallerys(new ShopGalleryParameters { Id = id }).SingleOrDefault();
        }

        public IQueryable<ShopGallery> FindAll(ShopGalleryParameters parameters, bool trackChanges)
        {
            return FindByCondition(a => (parameters.Id == 0 || a.Id == parameters.Id) &&
                                        (parameters.Fk_Shop == 0 || a.Fk_Shop == parameters.Fk_Shop), trackChanges);
        }

        public async Task<ShopGallery> FindById(int id, bool trackChanges)
        {
            return id == 0 ? null
                         : await FindByCondition(a => a.Id == id, trackChanges: trackChanges)
                         .SingleOrDefaultAsync();
        }

        public async Task<string> UploadShopGalleryImages(string rootPath, IFormFile file)
        {
            FileUploader uploader = new(rootPath);
            return await uploader.UploudFile(file, "Uploud/Shop/Gallery");
        }
    }


}
