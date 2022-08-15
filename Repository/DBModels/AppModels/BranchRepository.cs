using Entities.DBModels.AppModels;
using Entities.DtoModels.AppModels;

namespace Repository.DBModels.AppModels
{
    public class BranchRepository : RepositoryBase<Branch>
    {
        protected readonly IMapper _mapper;

        public BranchRepository(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public IQueryable<BranchDto> GetBranchs(RequestParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new BranchDto
                   {
                       Id = a.Id,
                       Name = a.Name,
                       CreatedAtVal = a.CreatedAt,
                       ShopCount = a.Shops.Count
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
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
    }
}
