using Application.Services;
using Application.Services.JWT;
using Domain.Entities;
using Domain.Entities.JWT;
using Domain.Interfaces;
using Domain.Interfaces.JWT;
using Domain.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Mappings;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(opcion =>
{
    opcion.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Siniestros viales",
        Description = "Facilitar la gestión y análisis de siniestros viales, ofreciendo una API flexible y escalable que pueda integrarse con sistemas de reporte, visualización o análisis estadístico.",
        Contact = new OpenApiContact
        {
            Name = "Sebastian Veloza",
            Url = new Uri("https://github.com/SebastianVeloza")

        }
    });

});

builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Cors
var corsUris = builder.Configuration.GetSection("JwtSettings:ClientsUris").Get<List<string>>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(corsUris.ToArray())
              .AllowCredentials()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
#endregion

#region Logs
var loggerRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
log4net.Config.XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));

#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(AutoMapperProfile).Assembly);
#endregion

#region JWT
builder.Services.AddScoped<AuthService>(); // Registrar AuthService
builder.Services.AddScoped<RefreshTokenService>();
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true
    };

});



#endregion

#region Conexion BD
builder.Services.AddDbContextFactory<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.CommandTimeout(3000);
    }));
#endregion

#region MediatR

builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));
#endregion

#region Servicios

builder.Services.AddTransient(typeof(IAppLogger<>), typeof(WebAPI.Log4NetAppLogger<>));


#endregion
#region HttpOnly Cookie
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Usar siempre HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict;
});
#endregion

#region Repositorios y UOW
builder.Services.AddScoped<IciudadesRepository, ciudadesRepository>();
builder.Services.AddScoped<IdepartamentosRepository, departamentosRepository>();
builder.Services.AddScoped<Ihistorico_refresh_tokenRepository, historico_Refresh_TokenRepository>();
builder.Services.AddScoped<ILogs_SiniestrosRepository, Logs_SiniestrosRepository>();
builder.Services.AddScoped<ISiniestrosRepository, siniestrosRepository>();
builder.Services.AddScoped<Itipos_siniestroRepository, tipos_SiniestroRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar CORS
app.UseCors("AllowSpecificOrigin");
// Middleware de manejo de errores
app.UseMiddleware<ErrorHandlingMiddleware>();
// Usar el middleware JWT para la autenticación
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
