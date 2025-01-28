var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); //  this add all controllers classes as a service

var app = builder.Build();
app.UseStaticFiles();
app.MapControllers(); //  this works as a app.UseRouting() And app.UseEndPoint Together

app.Run();
