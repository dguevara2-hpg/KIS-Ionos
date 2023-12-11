using Azure.Identity;
using Azure.Core;
using KIS_Core.Domain.Models;
using KIS_Core.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });

//// Add app.settings configuration
//builder.Services.Configure<LibraryConfig>(
//builder.Configuration.GetSection(LibraryConfig.Position));
// string connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
// Add SQL connection
//builder.Configuration.GetConnectionString("DB");
var connectionString = builder.Configuration.GetConnectionString("DB");
builder.Services.AddTransient(a =>
{
   var sqlConnection = new SqlConnection(connectionString);
//     var credential = new DefaultAzureCredential();
//     var credential = new ManagedIdentityCredential("4b22eea5-79f3-4444-b353-cebafa5066a0");

//    var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));
//    sqlConnection.AccessToken = token.Token;
    return sqlConnection;
});

// Add Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(2);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();