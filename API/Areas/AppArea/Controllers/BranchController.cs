using Entities.DBModels.AppModels;
using Entities.DtoModels.AppModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Areas.AppArea.Controllers
{
    [ApiVersion("1.0")]
    [Area("App")]
    [ApiExplorerSettings(GroupName = "App")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    public class BranchController : ExtendControllerBase
    {
        public BranchController(
       IMapper mapper,
       RepositoryManager repositoryManager,
       LinkGenerator linkGenerator,
       IWebHostEnvironment environment) : base(mapper, repositoryManager, linkGenerator, environment)
        { }

        [HttpGet]
        [Route(nameof(GetBranchs))]
        public async Task<IEnumerable<BranchDto>> GetBranchs([FromQuery] RequestParameters parameters)
        {
            PagedList<BranchDto> branches = await _repositoryManager.Branch.GetBranchsPaged(parameters);

            SetPagination(branches.MetaData, parameters);

            return branches;
        }
        [HttpGet]
        [Route(nameof(GetBranchById))]
        public BranchDto GetBranchById([FromQuery, BindRequired] int id)
        {
            return _repositoryManager.Branch.GetBranchbyId(id);
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<BranchDto> Create([FromBody] BranchCreateOrEditDto model)
        {
            AuthenticatedDto auth = (AuthenticatedDto)Request.HttpContext.Items[ApiConstants.Authenticated];

            Branch branch = _mapper.Map<Branch>(model);

            branch.CreatedBy = auth.Name;

            if (model.BranchImageFile != null)
            {
                branch.ImageUrl = await _repositoryManager.Branch.UploadBranchImage(_environment.WebRootPath, model.BranchImageFile);
                branch.StorageUrl = GetBaseUri();
            }

            _repositoryManager.Branch.Create(branch);
            await _repositoryManager.Save();

            return _repositoryManager.Branch.GetBranchbyId(branch.Id);
        }

        [HttpPut]
        [Route(nameof(Edit))]
        public async Task<BranchDto> Edit(
            [FromQuery, BindRequired] int id,
            [FromBody] BranchCreateOrEditDto model)
        {
            AuthenticatedDto auth = (AuthenticatedDto)Request.HttpContext.Items[ApiConstants.Authenticated];

            Branch branch = await _repositoryManager.Branch.FindById(id, trackChanges: true);

            _mapper.Map(model, branch);

            branch.LastModifiedBy = auth.Name;

            if (model.BranchImageFile != null)
            {
                branch.ImageUrl = await _repositoryManager.Branch.UploadBranchImage(_environment.WebRootPath, model.BranchImageFile);
                branch.StorageUrl = GetBaseUri();
            }
            await _repositoryManager.Save();

            return _repositoryManager.Branch.GetBranchbyId(branch.Id);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<bool> Delete([FromQuery, BindRequired] int id)
        {
            Branch branch = await _repositoryManager.Branch.FindById(id, trackChanges: true);

            _repositoryManager.Branch.Delete(branch);
            await _repositoryManager.Save();

            return true;
        }
    }
}
