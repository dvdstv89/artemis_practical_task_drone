using drones.API.Data;
using drones.API.Repositories;
using drones.API.Services;
using drones.API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DroneApiDbContext>(options =>
{
    options.UseInMemoryDatabase("DroneDB");
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();

builder.Services.AddScoped<IDroneRepository, DroneRepository>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<IDroneService, DroneService>();
builder.Services.AddScoped<IDroneMedicationRepository, DroneMedicationRepository>();
builder.Services.AddScoped<IPeriodicTaskLogService, PeriodicTaskLogService>();
builder.Services.AddScoped<IPeriodicTaskLogRepository, PeriodicTaskLogRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Drones API",
            Version = "v1"
        });
});

builder.Services.AddHostedService<PeriodicHostedService>(provider =>
{
    return new PeriodicHostedService(
        provider.GetRequiredService<ILogger<PeriodicHostedService>>(),
        provider.GetRequiredService<IServiceScopeFactory>()
    );
});

var app = builder.Build();

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DroneApiDbContext>();
    DataSeeder.Initialize(services);
}


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
