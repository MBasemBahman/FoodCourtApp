
using Entities.DBModels.AppModels;
using Entities.DtoModels.AppModels;

namespace Repository.DBModels.AppModels
{
    public class AppAttachmentRepository : RepositoryBase<AppAttachment>
    {
        protected readonly IMapper _mapper;

        public AppAttachmentRepository(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public IQueryable<AppAttachmentDto> GetAppAttachments(RequestParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new AppAttachmentDto
                   {
                       Id = a.Id,
                       CreatedAtVal = a.CreatedAt,
                       FileLength=a.FileLength,
                       FileName=a.FileName,
                       FileType=a.FileType,
                       FileUrl=a.FileUrl,
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
        }

        public async Task<PagedList<AppAttachmentDto>> GetAppAttachmentsPaged(
          RequestParameters parameters)
        {
            return await PagedList<AppAttachmentDto>.ToPagedList(GetAppAttachments(parameters), parameters.PageNumber, parameters.PageSize);
        }

        public AppAttachmentDto GetAppAttachmentbyId(int id)
        {
            return GetAppAttachments(new RequestParameters { Id = id }).SingleOrDefault();
        }

        public IQueryable<AppAttachment> FindAll(RequestParameters parameters, bool trackChanges)
        {
            return FindByCondition(a => parameters.Id == 0 || a.Id == parameters.Id, trackChanges);
        }

        public async Task<AppAttachment> FindById(int id, bool trackChanges)
        {
            return id == 0 ? null
                         : await FindByCondition(a => a.Id == id, trackChanges: trackChanges)
                         .SingleOrDefaultAsync();
        }
    }

}
