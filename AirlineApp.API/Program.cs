using AirlineApp.Api.Middlewares.Extensions;
using AirlineApp.Application.Mappers;
using AirlineApp.Application.Services;
using AirlineApp.Contracts.Dtos.AircraftFamilyDtos;
using AirlineApp.Contracts.Dtos.AircraftModelDtos;
using AirlineApp.Contracts.Dtos.FlightDtos;
using AirlineApp.Contracts.Dtos.PassengerDtos;
using AirlineApp.Contracts.Dtos.TicketDtos;
using AirlineApp.Contracts.Interfaces;
using AirlineApp.Contracts.Validators;
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
builder.Services.AddScoped<ICrudService<AircraftFamilyGetDto, AircraftFamilyEditDto>, AircraftFamilyService>();
builder.Services.AddScoped<ICrudService<AircraftModelGetDto, AircraftModelEditDto>, AircraftModelService>();
builder.Services.AddScoped<ICrudService<FlightGetDto, FlightEditDto>, FlightService>();
builder.Services.AddScoped<ICrudService<PassengerGetDto, PassengerEditDto>, PassengerService>();
builder.Services.AddScoped<ICrudService<TicketGetDto, TicketEditDto>, TicketService>();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<AircraftFamilyEditDto>, AircraftFamilyEditDtoValidator>();
builder.Services.AddTransient<IValidator<AircraftModelEditDto>, AircraftModelEditDtoValidator>();
builder.Services.AddTransient<IValidator<FlightEditDto>, FlightEditDtoValidator>();
builder.Services.AddTransient<IValidator<PassengerEditDto>, PassengerEditDtoValidator>();
builder.Services.AddTransient<IValidator<TicketEditDto>, TicketEditDtoValidator>();

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

    await context.Database.MigrateAsync();

    var seeder = new DbSeederForDb(context);
    await seeder.SeedAsync(forceReset: false);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();