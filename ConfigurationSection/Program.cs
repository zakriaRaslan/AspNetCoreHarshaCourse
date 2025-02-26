var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.Map("/", async context =>
//    {
//        await context.Response.WriteAsync(builder.Configuration["MyKey"] + "\n");

//        await context.Response.WriteAsync(builder.Configuration.GetValue<string>("MyKey") + "\n"); 
        
//        await context.Response.WriteAsync(builder.Configuration.GetValue<string>("MyKey1" , "The Default Value")); 
//    });
//});

app.MapControllers();


app.Run();
