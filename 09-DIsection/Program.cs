using ServicesContract;
using ServicesLayer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    ServiceLifetime.Transient
//    ));
builder.Services.AddScoped<ICitiesService , CitiesService>();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
