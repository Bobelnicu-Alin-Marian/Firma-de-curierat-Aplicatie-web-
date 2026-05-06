
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
builder.Services.AddScoped<IStatusLivrareService, StatusLivrareService>();
builder.Services.AddScoped<IAdresaService, AdresaService>();
builder.Services.AddScoped<IContactService, ContactService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
  
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