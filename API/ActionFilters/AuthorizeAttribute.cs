namespace API.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAllAttribute>().Any())
            {
                return;
            }

            string validator = context.HttpContext.Request.Headers[HeadersConstants.Validator];

            if (string.IsNullOrWhiteSpace(validator))
            {
                context.Result = new BadRequestResult();
                return;
            }

            IServiceProvider services = context.HttpContext.RequestServices;
            JwtUtils _jwtUtils = services.GetService<JwtUtils>();
            if (_jwtUtils.ValidateJwtToken(validator) == null)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            AuthenticatedDto account = (AuthenticatedDto)context.HttpContext.Items[ApiConstants.Authenticated];
            if (account == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
