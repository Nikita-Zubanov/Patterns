using Microsoft.EntityFrameworkCore;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<ReadingShopContext>(options =>
        options.UseInMemoryDatabase(databaseName: "ReadingShop"), ServiceLifetime.Singleton)
    .AddSingleton<IReadProductContext, ReadingShopContext>((provider) => provider.GetService<ReadingShopContext>())
    .AddSingleton<ISynchronizeProductContext, ReadingShopContext>((provider) => provider.GetService<ReadingShopContext>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
