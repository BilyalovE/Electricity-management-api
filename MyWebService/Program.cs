using Microsoft.EntityFrameworkCore;
using MyWebService.Core.Services;
using MyWebService.Infrastructure.Repositories;
using Microsoft.OpenApi.Models; 

var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Добавляем DbContext с in-memory базой данных
builder.Services.AddDbContext<ElectricityDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Регистрация репозиториев и сервисов
builder.Services.AddScoped<IConsumptionObjectRepository, ConsumptionObjectRepository>();
builder.Services.AddScoped<ConsumptionObjectService>();

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<OrganizationService>();

builder.Services.AddScoped<IChildOrganizationRepository, ChildOrganizationRepository>();
builder.Services.AddScoped<ChildOrganizationService>();

builder.Services.AddScoped<ISupplyPointRepository, SupplyPointRepository>();
builder.Services.AddScoped<SupplyPointService>();

builder.Services.AddScoped<IMeasurementPointRepository, MeasurementPointRepository>();
builder.Services.AddScoped<MeasurementPointService>();

builder.Services.AddScoped<IElectricityMeterRepository, ElectricityMeterRepository>();
builder.Services.AddScoped<ElectricityMeterService>();

builder.Services.AddScoped<ICurrentTransformerRepository, CurrentTransformerRepository>();
builder.Services.AddScoped<CurrentTransformerService>();

builder.Services.AddScoped<IVoltageTransformerRepository, VoltageTransformerRepository>();
builder.Services.AddScoped<VoltageTransformerService>();

builder.Services.AddScoped<ICalculationMeterRepository, CalculationMeterRepository>();
builder.Services.AddScoped<CalculationMeterService>();

// Добавляем контроллеры
builder.Services.AddControllers();

// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Web Service API",
        Version = "v1",
        Description = "API for managing electricity-related entities",
        Contact = new OpenApiContact
        {
            Name = "Eldar",
            Email = "eldar2117@gmail.com"
        }
    });
});

var app = builder.Build();

// Включаем Swagger и Swagger UI только в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Включает генерацию JSON-документации
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web Service API v1");
    }); // Включает веб-интерфейс Swagger
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

await DbInitializer.Initialize(app.Services);

app.Run();