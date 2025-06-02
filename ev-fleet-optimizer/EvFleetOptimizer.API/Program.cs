using EvFleetOptimizer.Core.Interfaces;
using EvFleetOptimizer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EvFleetOptimizer.Infrastructure.Data;
using EvFleetOptimizer.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFleetRepository, FleetRepository>();
builder.Services.AddAutoMapper(typeof(EvFleetOptimizer.API.DTOs.MappingProfile).Assembly);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FleetDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// DEBUG: Check AutoMapper configuration at startup
using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<AutoMapper.IMapper>();
    try
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        Console.WriteLine("[AutoMapper] Configuration is valid.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[AutoMapper] Configuration error: {ex.Message}");
        throw;
    }
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

// Redirect root to Swagger UI
app.MapGet("/", context => {
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
