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

        public IQueryable<BranchDto> GetAccountTypes(RequestParameters parameters)
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

        public async Task<PagedList<BranchDto>> GetAccountTypesPaged(
          RequestParameters parameters)
        {
            return await PagedList<BranchDto>.ToPagedList(GetAccountTypes(parameters), parameters.PageNumber, parameters.PageSize);
        }

        public BranchDto GetAccountTypebyId(int id)
        {
            return GetAccountTypes(new RequestParameters { Id = id }).SingleOrDefault();
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
