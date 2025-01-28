using MiddlewareSection.CustomMiddlewares;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

// middleware 1 
app.Use(async (HttpContext context , RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello 1 from middlewar 1 \n ");
    await next(context);
});

app.UseWhen(context => context.Request.Query.ContainsKey("username"), app =>
{
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync(context.Request.Query["username"]);
        await next(context);
    });
});
// middleware 2
//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

// middlewar 3 
app.Run(async (HttpContext context) =>
{
    #region Stream Reader To Read Request Body
    //StreamReader streamReader = new StreamReader(context.Request.Body);
    //string body = await  streamReader.ReadToEndAsync();

    // Dictionary<string,StringValues> dictionaryBody = QueryHelpers.ParseQuery(body);

    //if (dictionaryBody.ContainsKey("name"))
    //{   
    //    await context.Response.WriteAsync(dictionaryBody["name"][0]);
    //}
    #endregion

    await context.Response.WriteAsync("Hello 3 from middleware 3 \n");

});


app.Run();
