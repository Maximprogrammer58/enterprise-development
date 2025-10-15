using AirlineApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirlineApp.Infrastructure.Persistence;

public static class DbSeederForDb
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // AircraftFamilies
        if (!context.AircraftFamilies.Any())
        {
            await context.AircraftFamilies.AddRangeAsync(new[]
            {
                new AircraftFamily { Name = "737", Manufacturer = "Boeing" },
                new AircraftFamily { Name = "A320", Manufacturer = "Airbus" }
            });
            await context.SaveChangesAsync(); // сохраняем, чтобы Id сгенерировались
        }

        // AircraftModels
        if (!context.AircraftModels.Any())
        {
            var family737 = await context.AircraftFamilies.FirstAsync(a => a.Name == "737");
            var familyA320 = await context.AircraftFamilies.FirstAsync(a => a.Name == "A320");

            await context.AircraftModels.AddRangeAsync(new[]
            {
                new AircraftModel { Name = "737-800", Family = family737, PassengerCapacity = 189, CargoCapacity = 2000, FlightRange = 5000 },
                new AircraftModel { Name = "A320-200", Family = familyA320, PassengerCapacity = 180, CargoCapacity = 1900, FlightRange = 6100 }
            });
            await context.SaveChangesAsync();
        }

        // Passengers
        if (!context.Passengers.Any())
        {
            await context.Passengers.AddRangeAsync(new[]
            {
                new Passenger { FullName = "John Doe", PassportNumber = "AA123456", BirthDate = new DateOnly(1990,1,1)  },
                new Passenger { FullName = "Jane Smith", PassportNumber = "BB654321", BirthDate = new DateOnly(1985,5,5) }
            });
            await context.SaveChangesAsync();
        }

        // Flights
        if (!context.Flights.Any())
        {
            var model737 = await context.AircraftModels.FirstAsync(m => m.Name == "737-800");

            await context.Flights.AddAsync(new Flight
            {
                Code = "F100",
                AircraftModel = model737,
                Departure = "NYC",
                Arrival = "LAX",
                DepartureDateTime = DateTime.UtcNow.AddDays(1),
                ArrivalDateTime = DateTime.UtcNow.AddDays(1).AddHours(6),
                Duration = TimeSpan.FromHours(6)
            });
            await context.SaveChangesAsync();
        }

        // Tickets
        if (!context.Tickets.Any())
        {
            var flight = await context.Flights.FirstAsync();
            var passenger = await context.Passengers.FirstAsync();

            await context.Tickets.AddAsync(new Ticket
            {
                Flight = flight,
                Passenger = passenger,
                SeatNumber = "12A",
                HasHandLuggage = true,
                BaggageWeight = 15
            });
            await context.SaveChangesAsync();
        }
    }
}