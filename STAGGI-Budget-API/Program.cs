using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STAGGI_Budget_API.Data;
using STAGGI_Budget_API.Models;
using STAGGI_Budget_API.Repositories;
using STAGGI_Budget_API.Repositories.Interfaces;
using STAGGI_Budget_API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BudgetContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

//Repositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IBudUserRepository, BUserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

//Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();


