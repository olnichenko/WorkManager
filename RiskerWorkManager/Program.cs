using RiskerWorkManager;

var builder = WebApplication.CreateBuilder(args);

// if is Development get setting from appsettings.<computer name>.json
// else standart settings
if (builder.Environment.EnvironmentName == "Development")
{
    builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true)
        .AddEnvironmentVariables();
}

// extensions Startup.cs
builder.Services.ConfigureServices();
builder.Services.MapSettings(builder.Configuration);
builder.Services.MapRepositories();
builder.Services.MapServices(builder.Configuration);

if (builder.Environment.EnvironmentName == "Development")
{
    builder.Services.AddSwaggerGen();
}
// Add services to the container.

builder.Services.AddControllersWithViews();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAllOrigins");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

if (builder.Environment.EnvironmentName == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
    });
}

app.Run();
