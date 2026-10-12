using NOVIX.Identity.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NOVIX.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NOVIX.Identity.Persistence.Contexts;
using NOVIX.Catalog.Persistence;
using NOVIX.Catalog.Persistence.Contexts;
using NOVIX.Catalog.Persistence.Seed;
using NOVIX.Catalog.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddIdentityPersistence(builder.Configuration);
builder.Services.AddIdentityInfrastructure();
builder.Services.AddCatalogPersistence(builder.Configuration);
builder.Services.AddCatalogInfrastructure();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(
        typeof(NOVIX.Catalog.Application.Products.Commands.CreateProduct.CreateProductCommand).Assembly,
        typeof(NOVIX.Identity.Application.Auth.Commands.Login.LoginCommand).Assembly
    );
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Database initialization is handled below inside a try-catch block

// Configure the HTTP request pipeline.
// Always enable Swagger UI even in Production so you can test it on MonsterASP
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<NOVIX.WebAPI.Middlewares.GlobalExceptionMiddleware>();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var identityContext = services.GetRequiredService<AppIdentityDbContext>();
        identityContext.Database.EnsureCreated();

        var catalogContext = services.GetRequiredService<CatalogDbContext>();
        
        try
        {
            var databaseCreator = catalogContext.Database.GetService<Microsoft.EntityFrameworkCore.Storage.IRelationalDatabaseCreator>();
            databaseCreator.CreateTables();
        }
        catch (Microsoft.Data.SqlClient.SqlException)
        {
            // Tables might already exist
        }
        
        await CatalogDbContextSeed.SeedAsync(catalogContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database migration or seeding.");
    }
}

app.Run();
