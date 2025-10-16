using AirlineApp.Application.Mappers;
using AirlineApp.Application.Services;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using AirlineApp.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAircraftFamilyRepository, AircraftFamilyRepository>();
builder.Services.AddScoped<IAircraftModelRepository, AircraftModelRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddAutoMapper(typeof(AppMappingProfile).Assembly);

builder.Services.AddTransient<DbSeederForDb>();
builder.Services.AddScoped<AnalyticsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    var seeder = new DbSeederForDb(context);
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
