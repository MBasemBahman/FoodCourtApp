
using Entities.DBModels.AppModels;
using Entities.DtoModels.AppModels;
using Microsoft.AspNetCore.Http;
using Services;

namespace Repository.DBModels.AppModels
{
    public class AppAttachmentRepository : RepositoryBase<AppAttachment>
    {

        public AppAttachmentRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<AppAttachmentDto> GetAppAttachments(AppAttachmentParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new AppAttachmentDto
                   {
                       Id = a.Id,
                       Fk_Branch = a.Fk_Branch,
                       CreatedAtVal = a.CreatedAt,
                       FileLength = a.FileLength,
                       FileName = a.FileName,
                       FileType = a.FileType,
                       FileUrl = a.StorageUrl + a.FileUrl,
                       Order = a.Order,
                       Branch = a.Fk_Branch != null ?
                       new BranchDto
                       {
                           Name = a.Branch.Name
                       } 
                       :null
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
        }

        public async Task<PagedList<AppAttachmentDto>> GetAppAttachmentsPaged(
          AppAttachmentParameters parameters)
        {
            return await PagedList<AppAttachmentDto>.ToPagedList(GetAppAttachments(parameters), parameters.PageNumber, parameters.PageSize);
        }

        public AppAttachmentDto GetAppAttachmentbyId(int id)
        {
            return GetAppAttachments(new AppAttachmentParameters { Id = id }).SingleOrDefault();
        }

        public IQueryable<AppAttachment> FindAll(AppAttachmentParameters parameters, bool trackChanges)
        {
            return FindByCondition(a => (parameters.Id == 0 || a.Id == parameters.Id) &&
                                        (parameters.Fk_Branch == 0 || a.Fk_Branch == parameters.Fk_Branch), trackChanges);
        }

        public async Task<AppAttachment> FindById(int id, bool trackChanges)
        {
            return id == 0 ? null
                         : await FindByCondition(a => a.Id == id, trackChanges: trackChanges)
                         .SingleOrDefaultAsync();
        }


        public async Task<string> UploadAppAttachments(string rootPath, IFormFile file)
        {
            FileUploader uploader = new(rootPath);
            return await uploader.UploudFile(file, "Uploud/AppAttachment");
        }
    }

}
