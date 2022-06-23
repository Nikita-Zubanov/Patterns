using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.WritingShop;
using Patterns.CQRSEventSourcing.Infrastucture.DbContexts.ReadingShop;
using Patterns.CQRSEventSourcing.WritingShop.Helpers;
using Patterns.CQRSEventSourcing.WritingShop.QuartzJobs;

var builder = WebApplication.CreateBuilder(args);
var sendEventStore = new SendEventStore();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<WriteShopEventsContext>(options =>
        options.UseInMemoryDatabase(databaseName: "WritingShop"),
        ServiceLifetime.Singleton)
    .AddSingleton<IWriteProductContext, WriteShopEventsContext>()
    .AddSingleton<IAppSettings, AppSettings>()
    .AddSingleton<SendEventStore>(sendEventStore)
    .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
    .AddQuartz(quartzConfigurator =>
    {
        quartzConfigurator.UseMicrosoftDependencyInjectionScopedJobFactory();

        var jobKey = new JobKey("SynchronizeEntitiesInReadingDbJob");
        quartzConfigurator.AddJob<SynchronizeEntitiesInReadingDbJob>(opts =>
            opts.WithIdentity(jobKey));

        quartzConfigurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("SynchronizeEntitiesInReadingDbJob_Trigger")
            .WithCronSchedule("0/10 * * * * ?"));
    })
    .AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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