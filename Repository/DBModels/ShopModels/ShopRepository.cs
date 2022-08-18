using Entities.DBModels.ShopModels;
using Entities.DtoModels.ShopModels;
using Microsoft.AspNetCore.Http;
using Services;

namespace Repository.DBModels.ShopModels
{
    public class ShopRepository : RepositoryBase<Shop>
    {
        public ShopRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<ShopDto> GetShops(ShopParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new ShopDto
                   {
                       Id = a.Id,
                       Name = a.Name,
                       SearchTxt = a.SearchTxt,
                       Address = a.Address,
                       GalleryCount = a.ShopGalleries.Count,
                       ImageUrl = a.StorageUrl + a.ImageUrl,
                       LastModifiedAtVal = a.LastModifiedAt,
                       LastModifiedBy = a.LastModifiedBy,
                       CreatedBy = a.CreatedBy,
                       CreatedAtVal = a.CreatedAt,
                       Branch = new Entities.DtoModels.AppModels.BranchDto
                       {
                           Name = a.Branch.Name
                       },
                       Order = a.Order
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
        }

        public async Task<PagedList<ShopDto>> GetShopsPaged(
          ShopParameters parameters)
        {
            return await PagedList<ShopDto>.ToPagedList(GetShops(parameters), parameters.PageNumber, parameters.PageSize);
        }

        public ShopDto GetShopbyId(int id)
        {
            return GetShops(new ShopParameters { Id = id }).SingleOrDefault();
        }

        public IQueryable<Shop> FindAll(ShopParameters parameters, bool trackChanges)
        {
            return FindByCondition(a => (parameters.Id == 0 || a.Id == parameters.Id) &&
                                        (parameters.Fk_Branch == 0 || a.Fk_Branch == parameters.Fk_Branch), trackChanges);
        }

        public async Task<Shop> FindById(int id, bool trackChanges)
        {
            return id == 0 ? null
                         : await FindByCondition(a => a.Id == id, trackChanges: trackChanges)
                         .SingleOrDefaultAsync();
        }

        public async Task<string> UploadShopImage(string rootPath, IFormFile file)
        {
            FileUploader uploader = new(rootPath);
            return await uploader.UploudFile(file, "Uploud/Shop/Logo");
        }
    }

}
