using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add the DB Contexts
builder.Services.AddSqlServer<DotNetEd.CoreAdmin.DemoAppDotNet6.Models.KisCoreContext>(builder.Configuration.GetConnectionString("Database"));

// add Core Admin
builder.Services.AddCoreAdmin();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseCoreAdminCustomTitle("KIS Admin");

// Required for Core Admin
app.MapDefaultControllerRoute();

app.Run();
