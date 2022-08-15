using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace API.Extensions
{
    public static class ExceptionMiddleExtensions
    {
        public static void ConfigureExceptionHandler(
            this IApplicationBuilder app)
        {
            _ = app.UseExceptionHandler(appError =>
            {
                appError.Run(context =>
                {
                    Response details = new();

                    IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        details = new Response
                        {
                            ErrorMessage = contextFeature.Error.Message.Length > 80 ? "Something error, please try again!" : contextFeature.Error.Message,
                            ExceptionMessage = contextFeature.Error.InnerException == null ? "" : StringChecker.HasArabicCharacters(contextFeature.Error.InnerException.Message) ? "" : contextFeature.Error.InnerException.Message
                        };
                    }

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.Headers.Add(HeadersConstants.Status, details.ToString());
                    return Task.CompletedTask;
                });
            });
        }
    }
}
