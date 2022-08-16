using Entities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ExtendControllerBase: Controller
    {
        protected readonly RepositoryManager _Repository;
        protected readonly IMapper _Mapper;
        protected readonly ISession _Session;
        protected readonly LocalizationService _Localizer;
        protected readonly ExceptionHandler _ExceptionHandler;
        protected readonly LinkGenerator _linkGenerator;
        protected readonly IWebHostEnvironment _environment;

        public ExtendControllerBase(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
          LocalizationService Localizer, LinkGenerator linkGenerator,
                 IWebHostEnvironment environment)
        {
            _Repository = Repository;
            _Mapper = Mapper;
            _Session = HttpContextAccessor.HttpContext.Session;
            _Localizer = Localizer;
            _ExceptionHandler = new ExceptionHandler(Localizer);
            _linkGenerator = linkGenerator;
            _environment = environment;
        }

        protected string GetBaseUri()
        {
            return _linkGenerator.GetUriByAction(HttpContext).GetBaseUri(HttpContext.Request.RouteValues["area"].ToString());
        }

    }
}
