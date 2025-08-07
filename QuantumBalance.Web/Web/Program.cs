using Abstracciones.Interfaces.Reglas;
using Microsoft.AspNetCore.Authentication.Cookies;
using Reglas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IConfiguracion, Configuracion>();

var app = builder.Build();
//Configuración Autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/cuenta/Login";
        options.LogoutPath = "/cuenta/Logout";
        options.AccessDeniedPath = "/cuenta/Accesodenegado";
    });

//Configuración Autorización
builder.Services.AddTransient<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<IAutorizacionFlujo, AutorizacionFlujo>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.AutorizacionClaims();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
