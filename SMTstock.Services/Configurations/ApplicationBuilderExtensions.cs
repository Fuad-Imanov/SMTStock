using Microsoft.AspNetCore.Builder;
using SMTstock.Services.Middlewares;

namespace SMTstock.Services.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
     
    }
}
