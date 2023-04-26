using CatalogInfoCommonLibrary.Factories;
using CatalogInfoCommonLibrary.Providers;
using CatalogInfoService.BackgroundServices;
using CatalogInfoService.Extensions;
using CatalogInfoService.Factories;
using CatalogInfoService.Providers;
using CatalogInfoService.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(j =>
{
    j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddScoped<ICatalogService, CatalogService>();

builder.Services.AddSingleton<ICommonCommandFactory, CommonCommandFactory>();
builder.Services.AddSingleton<IDbProvider, DbProvider>();
builder.Services.AddSingleton<IMailSendingProvider, MailSendingProvider>();

builder.Services.AddHostedService<YandexB2CCatalogSendingBackgroundService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddFileLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
