using Repository.DBModels.AppModels;
using Repository.DBModels.ShopModels;

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

        private AppAttachmentRepository _appAttachmentRepository;
        private BranchRepository _branchRepository;

        #endregion

        #region ShopModels

        private ShopGalleryRepository _shopGalleryRepository;

        private ShopRepository _shopRepository;

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
        public AppAttachmentRepository AppAttachment
        {
            get
            {
                _appAttachmentRepository ??= new AppAttachmentRepository(_dBContext, _mapper);
                return _appAttachmentRepository;
            }
        }

        #endregion

        #region ShopModels
        public ShopGalleryRepository ShopGallery
        {
            get
            {
                _shopGalleryRepository ??= new ShopGalleryRepository(_dBContext, _mapper);
                return _shopGalleryRepository;
            }
        }
        public ShopRepository Shop
        {
            get
            {
                _shopRepository ??= new ShopRepository(_dBContext, _mapper);
                return _shopRepository;
            }
        }

        #endregion

        #endregion
    }
}
