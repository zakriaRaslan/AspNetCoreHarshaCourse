using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServicesContract;
using ServicesLayer;

var builder = WebApplication.CreateBuilder(args);
// you need to Add Autofac & Autofac.Extensions From Nuget Packages
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllersWithViews();
//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    ServiceLifetime.Transient
//    ));
//builder.Services.AddScoped<ICitiesService , CitiesService>();

#region Using Autofac To Register Services

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // this like AddTransient
    //container.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency();

    // this like AddScopped
    container.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope();

    //this like AddSingelton
    //container.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance();
});

#endregion


var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
