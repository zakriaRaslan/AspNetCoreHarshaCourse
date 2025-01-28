
namespace MiddlewareSection.CustomMiddlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync(" My Custom Middleware Start \n");
            await next(context);
            await context.Response.WriteAsync(" My Custom Middleware Ends \n ");
        }
    }

    public static class MyCustomMiddlewareExtention
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
