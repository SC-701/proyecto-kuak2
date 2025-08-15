using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using DA;
using DA.Repositorios;
using Flujo;
using Reglas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Autorizacion.Middleware;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//Autenticacion
var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
if (tokenConfiguration == null)
{
    throw new InvalidOperationException("La configuraci�n del token no est� presente en appsettings.json");
}

var jwtIssuer = tokenConfiguration.Issuer;
var jwtAudience = tokenConfiguration.Audience;
var jwtKey = tokenConfiguration.key;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
    );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();

// DA
builder.Services.AddScoped<ICuentaDA, CuentaDA>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<ICategoriaDA, CategoriaDA>();
builder.Services.AddScoped<ICuentaCategoriaDA, CuentaCategoriaDA>();
builder.Services.AddScoped<IMovimientoDA, MovimientoDA>();
builder.Services.AddScoped<ITipoMovimientoDA, TipoMovimientoDA>();

// Flujo 
builder.Services.AddScoped<ICuentaFlujo, CuentaFlujo>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<ICategoriaFlujo, CategoriaFlujo>();
builder.Services.AddScoped<ICuentaCategoriaFlujo, CuentaCategoriaFlujo>();
builder.Services.AddScoped<IMovimientoFlujo, MovimientoFlujo>();
builder.Services.AddScoped<ITipoMovimientoFlujo, TipoMovimientoFlujo>();

//Autorizaci�n
builder.Services.AddTransient<Autorizacion.Abstracciones.Flujo.IAutorizacionFlujo, Autorizacion.Flujo.AutorizacionFlujo>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA, Autorizacion.DA.SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, Autorizacion.DA.Repositorios.RepositorioDapper>();


//Configuraci�n de CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", policy =>
    {
        policy.WithOrigins("https://localhost:7227") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowWebApp");
app.AutorizacionClaims();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
