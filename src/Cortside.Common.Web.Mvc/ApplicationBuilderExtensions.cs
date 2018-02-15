using Microsoft.AspNetCore.Builder;

namespace Cortside.Common.Web.Mvc {

    public static class ApplicationBuilderExtensions {

        public static IApplicationBuilder UseHttpException(this IApplicationBuilder application) {
            return application.UseMiddleware<HttpExceptionMiddleware>();
        }

        public static IApplicationBuilder UseGenericExceptionHandler(this IApplicationBuilder application) {
            return application.UseMiddleware<GenericExceptionMiddleware>();
        }
    }
}
