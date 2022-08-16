using Repository.DBModels.AppModels;
using Repository.DBModels.AuthModels;
using Repository.DBModels.ShopModels;

namespace Repository
{
    public class RepositoryManager
    {
        private readonly DBContext _dBContext;

        public RepositoryManager(DBContext dBContext)
        {
            _dBContext = dBContext;
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

        #region Auth Models
        private SystemUserRepository _systemUserRepository;

        #endregion
        #endregion

        #region Public

        #region AppModels

        public BranchRepository Branch
        {
            get
            {
                _branchRepository ??= new BranchRepository(_dBContext);
                return _branchRepository;
            }
        }
        public AppAttachmentRepository AppAttachment
        {
            get
            {
                _appAttachmentRepository ??= new AppAttachmentRepository(_dBContext);
                return _appAttachmentRepository;
            }
        }

        #endregion

        #region ShopModels
        public ShopGalleryRepository ShopGallery
        {
            get
            {
                _shopGalleryRepository ??= new ShopGalleryRepository(_dBContext);
                return _shopGalleryRepository;
            }
        }
        public ShopRepository Shop
        {
            get
            {
                _shopRepository ??= new ShopRepository(_dBContext);
                return _shopRepository;
            }
        }

        #endregion

        #region Auth Models
        public SystemUserRepository SystemUser
        {
            get
            {
                _systemUserRepository ??= new SystemUserRepository(_dBContext);
                return _systemUserRepository;
            }
        }

        #endregion

        #endregion
    }
}
