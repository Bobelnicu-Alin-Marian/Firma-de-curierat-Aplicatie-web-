using FirmaCurierat.Models;
using FirmaCurierat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// inregistrarea Contextului 
builder.Services.AddDbContext<FirmaCurieratContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CurieratDbConnection")));

// suport pentru Roluri
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FirmaCurieratContext>();

// === AICI SUNT CONSTRÂNGERILE EXACTE DIN DOCUMENTUL PROFESORULUI ===
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});
// ====================================================================

builder.Services.AddScoped<FirmaCurierat.Services.IColetService, FirmaCurierat.Services.ColetService>();
builder.Services.AddScoped(typeof(FirmaCurierat.Repositories.IRepository<>), typeof(FirmaCurierat.Repositories.GenericRepository<>));
builder.Services.AddScoped<FirmaCurierat.Services.IHubService, FirmaCurierat.Services.HubService>();
builder.Services.AddScoped<ITarifService, TarifService>();
builder.Services.AddScoped<IStatusLivrareService, StatusLivrareService>();
builder.Services.AddScoped<IAdresaService, AdresaService>();
builder.Services.AddScoped<IContactService, ContactService>();

// === AICI ÎNREGISTRĂM NOUL SERVICIU DE AUTENTIFICARE (PENTRU CELE 3 PUNCTE) ===
builder.Services.AddScoped<IAuthService, AuthService>();
// ===============================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();