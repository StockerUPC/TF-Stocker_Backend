//IAM Bounded Context Injection Configuration
using Stocker_API.IAM.Application.Internal.CommandServices;
using Stocker_API.IAM.Application.Internal.OutboundServices;
using Stocker_API.IAM.Application.Internal.QueryServices;
using Stocker_API.IAM.Domain.Repositories;
using Stocker_API.IAM.Domain.Services;
using Stocker_API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Stocker_API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Stocker_API.IAM.Infrastructure.Tokens.JWT.Services;
using Stocker_API.IAM.Interfaces.ACL;
using Stocker_API.IAM.Interfaces.ACL.Services;
//Profiles Bounded Context Injection Configuration
using Stocker_API.Profiles.Application.Internal.CommandServices;
using Stocker_API.Profiles.Application.Internal.QueryServices;
using Stocker_API.Profiles.Domain.Repositories;
using Stocker_API.Profiles.Domain.Services;
using Stocker_API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using Stocker_API.Profiles.Interfaces.ACL;
using Stocker_API.Profiles.Interfaces.ACL.Services;
//Inventory Bounded Context Injection Configuration
using Stocker_API.Inventory.Application.Internal.CommandServices;
using Stocker_API.Inventory.Application.Internal.QueryServices;
using Stocker_API.Inventory.Domain.Repositories;
using Stocker_API.Inventory.Domain.Services;
using Stocker_API.Inventory.Infrastructure.Persistence.EFC.Repositories;
//Sales Bounded Context Injection Configuration
using Stocker_API.Sales.Application.Internal.CommandServices;
using Stocker_API.Sales.Application.Internal.QueryServices;
using Stocker_API.Sales.Domain.Repositories;
using Stocker_API.Sales.Domain.Services;
using Stocker_API.Sales.Infrastructure.Persistence.EFC.Repositories;
//Purchase Bounded Context Injection Configuration
using Stocker_API.Purchases.Application.Internal.CommandServices;
using Stocker_API.Purchases.Application.Internal.QueryServices;
using Stocker_API.Purchases.Domain.Repositories;
using Stocker_API.Purchases.Domain.Services;
using Stocker_API.Purchases.Infrastructure.Persistence.EFC.Repositories;
//Shared Bounded Context Injection Configuration
using Stocker_API.Shared.Domain.Repositories;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Stocker_API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Stocker_API.Shared.Interfaces.ASP.Configuration;
using Stocker_API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Stocker_API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Stocker_API",
                Version = "v1",
                Description = "Stocker API",
                TermsOfService = new Uri("https://stocker.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Stocker",
                    Email = "contact@stocker.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
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

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Publishing Bounded Context Injection Configuration
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

// Profiles Bounded Context Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

//Sales Bounded Context Injection Configuration
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientCommandService, ClientCommandService>();
builder.Services.AddScoped<IClientQueryService, ClientQueryService>();
builder.Services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
builder.Services.AddScoped<ISaleDetailCommandService, SaleDetailCommandService>();
builder.Services.AddScoped<ISaleDetailQueryService, SaleDetailQueryService>();

//Purchases Bounded Context Injection Configuration
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseCommandService, PurchaseCommandService>();
builder.Services.AddScoped<IPurchaseQueryService, PurchaseQueryService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<IPurchaseDetailRepository, PurchaseDetailRepository>();
builder.Services.AddScoped<IPurchaseDetailCommandService, PurchaseDetailCommandService>();
builder.Services.AddScoped<IPurchaseDetailQueryService, PurchaseDetailQueryService>();

// IAM Bounded Context Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");
// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();