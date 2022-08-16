using Entities.DBModels.AppModels;
using Entities.DtoModels.AppModels;
using Microsoft.AspNetCore.Http;
using Services;

namespace Repository.DBModels.AppModels
{
    public class BranchRepository : RepositoryBase<Branch>
    {

        public BranchRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<BranchDto> GetBranchs(RequestParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new BranchDto
                   {
                       Id = a.Id,
                       Name = a.Name,
                       CreatedAtVal = a.CreatedAt,
                       CreatedBy = a.CreatedBy,
                       LastModifiedBy = a.LastModifiedBy,
                       ShopCount = a.Shops.Count,
                       ImageUrl = a.StorageUrl + a.ImageUrl
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
        }

        public Dictionary<string,string> GetBranchesLoopUp(RequestParameters parameters)
        {
            return GetBranchs(parameters).ToDictionary(a => a.Id.ToString(), a => a.Name);
        }

        public async Task<PagedList<BranchDto>> GetBranchsPaged(
          RequestParameters parameters)
        {
            return await PagedList<BranchDto>.ToPagedList(GetBranchs(parameters), parameters.PageNumber, parameters.PageSize);
        }

        public BranchDto GetBranchbyId(int id)
        {
            return GetBranchs(new RequestParameters { Id = id }).SingleOrDefault();
        }

        public IQueryable<Branch> FindAll(RequestParameters parameters, bool trackChanges)
        {
            return FindByCondition(a => parameters.Id == 0 || a.Id == parameters.Id, trackChanges);
        }

        public async Task<Branch> FindById(int id, bool trackChanges)
        {
            return id == 0 ? null
                         : await FindByCondition(a => a.Id == id, trackChanges: trackChanges)
                         .SingleOrDefaultAsync();
        }
        public async Task<string> UploadBranchImage(string rootPath, IFormFile file)
        {
            FileUploader uploader = new(rootPath);
            return await uploader.UploudFile(file, "Uploud/Branch/Logo");
        }
    }
}
