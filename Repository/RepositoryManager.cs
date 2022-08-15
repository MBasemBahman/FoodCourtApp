using Repository.DBModels.AppModels;

namespace Repository
{
    public class RepositoryManager
    {
        private readonly DBContext _dBContext;
        protected readonly IMapper _mapper;

        public RepositoryManager(DBContext dBContext, IMapper mapper)
        {
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public async Task Save()
        {
            _ = await _dBContext.SaveChangesAsync();
        }

        #region Private

        #region AppModels

        private BranchRepository _branchRepository;

        #endregion

        #endregion

        #region Public

        #region AppModels

        public BranchRepository Branch
        {
            get
            {
                _branchRepository ??= new BranchRepository(_dBContext, _mapper);
                return _branchRepository;
            }
        }

        #endregion

        #endregion
    }
}
