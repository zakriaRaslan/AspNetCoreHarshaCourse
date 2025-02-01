using ModelValidationExapmle.CustomModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    //options.ModelBinderProviders.Insert(0, new PersonBinderProvider());
});
//To make the controller to read the XML Data You need to use XmlSerilizer
builder.Services.AddControllers().AddXmlSerializerFormatters();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
