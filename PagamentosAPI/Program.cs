using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PagamentosApi.Data;
using PagamentosApi.Data.Dtos;
using PagamentosApi.Models;
using PagamentosApi.Services;
using PagamentosApi.Services.Interfaces;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("PagamentoConnection");
builder.Services.AddCustomDbContexts(connectionString);

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "PagamentosApi", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opts.IncludeXmlComments(xmlPath);

    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Bearer Token para autenticação",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Description = "Insert the JWT Token",
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header
    };
    var requiremente = new OpenApiSecurityRequirement
    {
        {
            scheme, new List<string>()
        }
    };
    opts.AddSecurityRequirement(requiremente);
});

builder.Services.AddScoped<IBoletoService<ReadBoletoDto>, BoletoService<ReadBoletoDto>>();
builder.Services.AddScoped<IBoletoService<ReadBoletoForBancoDto>, BoletoService<ReadBoletoForBancoDto>>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
