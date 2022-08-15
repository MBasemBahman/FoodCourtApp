using Entities.DtoModels.AppModels;

namespace API.Areas.AppArea.Controllers
{
    [ApiVersion("1.0")]
    [Area("App")]
    [ApiExplorerSettings(GroupName = "App")]
    [Route("[area]/v{version:apiVersion}/[controller]")]
    public class AppAttachmentController : ExtendControllerBase
    {
        public AppAttachmentController(
        IMapper mapper,
        RepositoryManager repositoryManager,
        LinkGenerator linkGenerator,
        IWebHostEnvironment environment) : base(mapper, repositoryManager, linkGenerator, environment)
        { }

        [HttpGet]
        [Route(nameof(GetAppAttachments))]
        public async Task<IEnumerable<AppAttachmentDto>> GetAppAttachments([FromQuery] RequestParameters parameters)
        {
            PagedList<AppAttachmentDto> appAttachments = await _repositoryManager.AppAttachment.GetAppAttachmentsPaged(parameters);

            SetPagination(appAttachments.MetaData, parameters);

            return appAttachments;
        }
    }
}
