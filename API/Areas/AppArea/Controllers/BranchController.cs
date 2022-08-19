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
        [Route(nameof(GetBranchsAsync))]
        public async Task<IEnumerable<BranchDto>> GetBranchsAsync([FromQuery] RequestParameters parameters)
        {
            IEnumerable<BranchDto> branches = await _repositoryManager.Branch.GetBranchs(parameters).ToListAsync();

            return branches;
        }
    }
}
