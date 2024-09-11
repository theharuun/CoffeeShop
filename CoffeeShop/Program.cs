using CoffeeShop.DbOperations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper;
using CoffeeShop.Middlewares;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    options.JsonSerializerOptions.IgnoreNullValues = true;

    // Yeni eklenen ayar
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Döngüsel referanslarý yok sayma
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CoffeeShopDbContext>(options => options.UseInMemoryDatabase(databaseName: "CoffeeShopDb"));
builder.Services.AddScoped<ICoffeeShopDbContext>(provider => provider.GetService<CoffeeShopDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IloggerService,ConsoleLogger>();


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services= scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.useCustomExceptionMiddleWare();

app.MapControllers();

app.Run();
