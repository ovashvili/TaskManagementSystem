using TaskManagementSystem.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace TaskManagementSystem.Infrastructure.Extensions
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
