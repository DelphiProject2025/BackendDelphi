using delphibackend;
using delphibackend.IAM.Application.Internal.CommandServices;
using delphibackend.IAM.Application.Internal.OutboundServices;
using delphibackend.IAM.Application.Internal.QueryServices;
using delphibackend.IAM.Domain.Repositories;
using delphibackend.IAM.Domain.Services;
using delphibackend.IAM.Infraestructure.Hashing.BCrypt.Services;
using delphibackend.IAM.Infraestructure.Persistence.EFC.Repositories;
using delphibackend.IAM.Infraestructure.Pipeline.Middleware.Extensions;
using delphibackend.IAM.Infraestructure.Tokens.JWT.Configurations;
using delphibackend.IAM.Infraestructure.Tokens.JWT.Services;
using delphibackend.IAM.Interfaces.ACL;
using delphibackend.IAM.Interfaces.ACL.Services;
using delphibackend.Shared.Domain.Repositories;
using delphibackend.Shared.Infraestructure.Interfaces.ASP.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using delphibackend.Shared.Infraestructure.Persistences.EFC.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using delphibackend.User.Application.Internal.CommandServices;
using delphibackend.User.Application.Internal.QueryServices;
using delphibackend.User.Domain.Repositories;
using delphibackend.User.Domain.Services;
using delphibackend.User.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer("Bearer", options =>
{
    var secret = builder.Configuration["TokenSettings:Secret"];
    if (string.IsNullOrEmpty(secret))
    {
        throw new ArgumentNullException(nameof(secret), "JWT Secret is not configured in appsettings.json.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});

// Configurar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
           .EnableDetailedErrors(builder.Environment.IsDevelopment());
});

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});

// Configurar opciones de enrutamiento
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Delphi API",
        Version = "v1",
        Description = "Delphi Platform API",
        TermsOfService = new Uri("http://localhost:5000/swagger/index.html"),
        Contact = new OpenApiContact
        {
            Name = "Bruno&Erick",
            Email = "u202214843@upc.edu.pe"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token with Bearer prefix",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configurar servicios personalizados
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IAuthUserRepository, AuthUserRepository>();
builder.Services.AddScoped<IAuthUserCommandService, AuthUserCommandService>();
builder.Services.AddScoped<IAuthUserQueryService, AuthUserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IParticipantQueryService, ParticipantQueryService>();
builder.Services.AddScoped<IParticipantCommandService, ParticipantCommandService>();
builder.Services.AddScoped<IHostRepository, HostRepository>();
builder.Services.AddScoped<IHostCommandService, HostCommandService>();
builder.Services.AddScoped<IHostQueryService, HostQueryService>();






var app = builder.Build();

// Log Server Addresses
var serverAddresses = app.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>();
foreach (var address in serverAddresses.Addresses)
{
    Console.WriteLine($"Listening on {address}");
}

// Ensure Database is Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
