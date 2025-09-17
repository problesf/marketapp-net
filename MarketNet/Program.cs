using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentValidation;
using MarketNet.Application.Behaviors;
using MarketNet.Application.Categories.Validators;
using MarketNet.Application.Common.Interfaces;
using MarketNet.Infraestructure.Auth.Handlers.Products;
using MarketNet.Infraestructure.Auth.Parameters;
using MarketNet.Infraestructure.Auth.Requirements.Products;
using MarketNet.Infraestructure.Auth.Services;
using MarketNet.Infraestructure.Auth.Services.Impl;
using MarketNet.Infraestructure.Persistence;
using MarketNet.Infraestructure.Persistence.Repositories;
using MarketNet.Infraestructure.Persistence.Repositories.Impl;
using MarketNet.Infrastructure.Auth;
using MarketNet.src.Infraestructure.Persistence.Repositories;
using MarketNet.WebApi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//AUTENTICACION//
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));
var issuer = builder.Configuration["Jwt:Issuer"]!;
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireCustomer", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Customer");
    });

    options.AddPolicy("RequireSeller", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Seller");
    });

    options.AddPolicy("ProductOwnerOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Seller");
        policy.AddRequirements(new ProductOwnerRequirement());
    });
});

builder.Services.AddScoped<IAuthorizationHandler, ProductOwnerHandler>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MarketNet API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Pon aquí tu JWT con el formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    c.ExampleFilters();

});


builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IPAttributeRepository, PAttributeRepositoryImpl>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BaseExceptionBehavior<,>));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<FluentValidationExceptionHandler>();
builder.Services.AddExceptionHandler<BaseExceptionHandler>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.Configure<JwtParameters>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenServiceImpl>();

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly
    );
});
var useTcPg = builder.Configuration.GetValue<bool>("UseTestcontainersPostgres");

if (builder.Environment.IsEnvironment("Testing") && !useTcPg)
{
    builder.Services.AddDbContext<AppDbContext>(o =>
        o.UseInMemoryDatabase("TestDb"));
}
else
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? builder.Configuration["ConnectionStrings:Default"]
             ?? throw new InvalidOperationException("Falta la cadena de conexión 'DefaultConnection'.");

    builder.Services.AddDbContext<AppDbContext>(o =>
        o.UseNpgsql(cs));
}

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
    /// <inheritdoc/>
    protected Program() { }
}