using AirlineApp.API.Middlewares;
using AirlineApp.Application.Mappers;
using AirlineApp.Application.Services;
using AirlineApp.Application.Validators;
using AirlineApp.Domain.Interfaces;
using AirlineApp.Infrastructure.Persistence;
using AirlineApp.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
builder.Services.AddScoped<AircraftFamilyService>();
builder.Services.AddScoped<AircraftModelService>();
builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<PassengerService>();
builder.Services.AddScoped<TicketService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AircraftFamilyEditDtoValidator>();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);
});

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
app.UseGlobalExceptionHandling();
app.Run();
