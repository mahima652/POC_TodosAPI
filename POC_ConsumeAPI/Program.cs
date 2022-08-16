using POC_ConsumeAPI.Helper;
using POC_ConsumeAPI.Middleware;
using Serilog;
using POC_ConsumeAPI.Data;
using Unity.Microsoft.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using POC_ConsumeAPI.Filter;

var builder = WebApplication.CreateBuilder(args);

// Configure the logging 
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddScoped<ITodoLocalServices, TodoLocalServices>();
builder.Services.AddScoped<ITodoServices, TodoServices>();
builder.Services.AddSingleton<DataContext>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<OperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
    
