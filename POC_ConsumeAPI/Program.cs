using POC_ConsumeAPI.Helper;
using POC_ConsumeAPI.Middleware;
using Serilog;
using POC_ConsumeAPI.Filter;
using POC_ConsumeAPI.Services.IServices;
using POC_ConsumeAPI.Model;

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
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<OperationFilter>();
});

// Configure the HTTP request pipeline.
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Add Custom middleware 
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
    
