using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.DTOs;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services;
using STAGGI_Budget_API.Services.Interfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adding Entity Framework Context
builder.Services.AddDbContext<BudgetContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// Adding Identity
builder.Services.AddIdentity<BUser, IdentityRole>()
    .AddEntityFrameworkStores<BudgetContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddControllers();
// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudUserRepository, BUserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Services
builder.Services.AddScoped<IAccountService, AccountService>();


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        policy =>
//        {
//            policy
//                .WithOrigins("http://localhost:4200, localhost:4200")
//                .AllowAnyMethod()
//                .AllowCredentials()
//                .SetIsOriginAllowed((host) => true)
//                .AllowAnyHeader();
//        });
//});

var app = builder.Build();

// DbInitializer
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BudgetContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await DbInitializer.Initialize(context, userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:3000") // Cambia esto con el origen correcto
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); // Habilita el envío de credenciales}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
