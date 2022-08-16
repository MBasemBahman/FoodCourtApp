namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [AllowAll]
    public class ExtendControllerBase : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly LinkGenerator _linkGenerator;
        protected readonly RepositoryManager _repositoryManager;
        protected readonly IWebHostEnvironment _environment;

        public ExtendControllerBase(
        IMapper mapper,
        RepositoryManager repositoryManager,
        LinkGenerator linkGenerator,
        IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _linkGenerator = linkGenerator;
            _environment = environment;
        }

        // Helper Methods
        protected void SetPagination(MetaData metaData, RequestParameters requestParameters)
        {
            string actionUri = _linkGenerator.GetUriByAction(HttpContext);

            Response.Headers.Add(key: HeadersConstants.Pagination, value: ReplaceCommna(MetaData.PaginationMetaData(metaData, requestParameters, actionUri)));
        }

        protected string GetBaseUri()
        {
            return _linkGenerator.GetUriByAction(HttpContext).GetBaseUri(HttpContext.Request.RouteValues["area"].ToString());
        }

        protected void SetToken(TokenResponse token)
        {
            Response.Headers.Add(key: HeadersConstants.Expires, value: ReplaceCommna(token.Expires));
            Response.Headers.Add(key: HeadersConstants.Authorization, value: ReplaceCommna(token.RefreshToken));
        }

        protected void SetRefresh(TokenResponse token)
        {
            Response.Headers.Add(key: HeadersConstants.SetRefresh, value: token.ToString());
        }

        protected string IpAddress()
        {
            // get source ip address for the current request
            return Request.Headers.ContainsKey("x-Forwarded-For")
                ? (string)Request.Headers["x-Forwarded-For"]
                : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        protected static string ReplaceCommna(string data)
        {
            return data.Replace(",", @"\002C");
        }
    }
}
