using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Auth;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Gastos;
using PracticaParcial.Models.Reserva;
using PracticaParcial.Models.Unidades;
using PracticaParcial.Models.Users;
using PracticaParcial.Persistence;
using PracticaParcial.Persistence.Auth;
using PracticaParcial.Persistence.Consorcios;

var builder = WebApplication.CreateBuilder(args);

using var db = new UnidadDbContext();
db.Database.Migrate();



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUnidadesLogica, UnidadesLogica>();
builder.Services.AddScoped<IReservaLogica, ReservaLogica>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IConsorcioService, ConsorcioService>();
builder.Services.AddScoped<IConsorcioRepository, ConsorcioRepository>();
builder.Services.AddScoped<IUnidadesLogica, UnidadesLogica>();
builder.Services.AddScoped<IReservaLogica, ReservaLogica>();
builder.Services.AddScoped<IGastosLogica, GastosLogica>();
builder.Services.AddScoped<IGuardarArchivoLogica, GuardarArchivoLogica>();

builder.Services.AddDbContext<UnidadDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
    });

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();
app.Run();
