using Entities.DtoModels.AppModels;

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
    }
}
