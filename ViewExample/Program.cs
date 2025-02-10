var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // this to map controllers and views

var app = builder.Build();

app.UseStaticFiles(); // this is to use static files in wwwroot folder
app.MapControllers(); //  this is to use routing in controllers

app.Run();
