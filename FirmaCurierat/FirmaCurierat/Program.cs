
using FirmaCurierat.Models;
using FirmaCurierat.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// inregistrarea Contextului 
builder.Services.AddDbContext<FirmaCurieratContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CurieratDbConnection")));

builder.Services.AddScoped<FirmaCurierat.Services.IColetService, FirmaCurierat.Services.ColetService>();
builder.Services.AddScoped(typeof(FirmaCurierat.Repositories.IRepository<>), typeof(FirmaCurierat.Repositories.GenericRepository<>));
builder.Services.AddScoped<FirmaCurierat.Services.IHubService, FirmaCurierat.Services.HubService>();
builder.Services.AddScoped<ITarifService, TarifService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();