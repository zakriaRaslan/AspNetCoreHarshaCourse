using RoutingSection.CustomConstrains;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(option => option.ConstraintMap.Add("months", typeof(MonthsCustomConstrain)));
var app = builder.Build();

// To get the Executed Endpoint

//app.Use(async (context, next) =>
//{
//    // this will return null because we use it before app.UseRouting();
//    Microsoft.AspNetCore.Http.Endpoint Endpoint = context.GetEndpoint();
//    await next(context);
//});

// Enable Routing 
app.UseRouting(); // this allow the the server to select the appropriate endpoint based on Http Method And Url

//app.Use(async (context, next) =>
//{
//    // this will return the EndPoint
//    Microsoft.AspNetCore.Http.Endpoint Endpoint = context.GetEndpoint();
//    await next(context);
//});

// Creating The end points
# region The Old way To use end Way Now We can Register the Route Directly 
app.UseEndpoints(endpoint =>
{
    //// Here to write Youre End Points

    //endpoint.Map("files/{filename}.{extension}", async (context) =>
    //{
    //    string? fileName = context.Request.RouteValues["filename"].ToString();
    //    string? fileExtension = context.Request.RouteValues["extension"].ToString();

    //    await context.Response.WriteAsync($"Your File Is {fileName}.{fileExtension}");
    //});

    //endpoint.Map("employee/profile/{employeeName}", async (context) =>
    //{
    //    string? employeName = context.Request.RouteValues["employeeName"].ToString();
 

    //    await context.Response.WriteAsync($"The Employee Name is {employeName}");
    //});


    #region The Different between Map & Map{Method}
    //endpoint.Map("map1", async (context) =>
    //{
    //    // this endPoint will work with any method like get , post , put , etc 
    //    await context.Response.WriteAsync("MAP 1 Start");
    //});

    //endpoint.Map("map2", async (context) =>
    //{
    //    await context.Response.WriteAsync("MAP 2 start");
    //});

    ////  To make A specific endPoint For a Specific method
    //endpoint.MapGet("getmap", async (context) =>
    //{
    //    await context.Response.WriteAsync("Get Map Starts");
    //});

    //endpoint.MapPost("postmap", async (context) =>
    //{
    //    await context.Response.WriteAsync("Post Map Starts");
    //});
    #endregion
});
#endregion

#region Register The Route Directly at The Top-Level And Continue Route Section

app.Map("files/{filename}.{extension}", async (context) =>
{
    string? fileName = context.Request.RouteValues["filename"]?.ToString();
    string? fileExtension = context.Request.RouteValues["extension"]?.ToString();

    await context.Response.WriteAsync($"Your File Is {fileName}.{fileExtension}");
});

app.Map("employee/profile/{employeeName}", async (context) =>
{
    string? employeName = context.Request.RouteValues["employeeName"]?.ToString();

    await context.Response.WriteAsync($"The Employee Name is {employeName}");
});

#region Example about {Default Route Parameter} => {parameterName=parameterValue}
//app.Map("product/details/{id=1}", async (context) =>
//{
//    int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
//    await context.Response.WriteAsync(id==1 ?$"Your Product Id is the default Value  {id}" : $"Your product id  {id} ");
//});
#endregion

#region Example For optional paramater =>{paramter?} and use (int) constrain 
app.Map("product/details/{id:int?}", async (context) =>
{
    // by using int constrain with the route parameter it will not accept any value type else int 
    int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
    // the Convert.ToInt32 Will Convert null value to 0 so in condition will use != 0 not != null
    await context.Response.WriteAsync(id != 0 ? $"Your product id  {id}" : $"The Product Id Is not Supplied");
    
});

#endregion

#region Route Constrains
// we have a lot of constrains like int , bool , datetime , alpha , Guid , min() , max() , length(min , max) ,maxlength() , minlength() , regix and more
// datetime constrain
app.Map("daily-digest-report/{repordate:datetime}", async (context) =>
{
    DateTime reportDatetime = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
    await context.Response.WriteAsync($"Daily digest report date is => {reportDatetime}");
});

//Guid Constrain
app.Map("city/information/{cityid:guid}", async (context) =>
{
    Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);

    await context.Response.WriteAsync($"The City Id => {cityId}");
});

// Use Custom Constrains 
app.Map("sales/{year:int:min(1900)}/{month:months=apr}", async (context) =>
{
    string month = Convert.ToString(context.Request.RouteValues["month"])!;
    await context.Response.WriteAsync($"The Sales in {month}"); 
});

#endregion




#endregion
app.Run(async (context) =>
{
    await context.Response.WriteAsync($"The Recuest path is {context.Request.Path}");
});

app.Run();
