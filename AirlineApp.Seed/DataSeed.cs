using AirlineApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace AirlineApp.Seed;

/// <summary>
/// Provides seeded data for testing or demonstration purposes,
/// including aircraft families, models, flights, passengers, and tickets.
/// </summary>
public class DataSeed
{
    public DateTime BaseDate { get; }

    public DataSeed(DateTime? baseDate = null)
    {
        BaseDate = baseDate ?? new DateTime(2025, 9, 1);
    }

    public List<AircraftFamily> GetAircraftFamilies() => new()
    {
        new AircraftFamily { Id = 1, Name = "A320 Family", Manufacturer = "Airbus" },
        new AircraftFamily { Id = 2, Name = "737 Family", Manufacturer = "Boeing" },
        new AircraftFamily { Id = 3, Name = "777 Family", Manufacturer = "Boeing" },
        new AircraftFamily { Id = 4, Name = "787 Dreamliner", Manufacturer = "Boeing" },
        new AircraftFamily { Id = 5, Name = "A330 Family", Manufacturer = "Airbus" },
        new AircraftFamily { Id = 6, Name = "A350 Family", Manufacturer = "Airbus" },
        new AircraftFamily { Id = 7, Name = "CRJ Series", Manufacturer = "Bombardier" },
        new AircraftFamily { Id = 8, Name = "E-Jet Series", Manufacturer = "Embraer" },
        new AircraftFamily { Id = 9, Name = "MC-21 Family", Manufacturer = "Irkut" },
        new AircraftFamily { Id = 10, Name = "SSJ100 Family", Manufacturer = "Sukhoi" }
    };

    public List<AircraftModel> GetAircraftModels(List<AircraftFamily> families) => new()
    {
        new AircraftModel { Id = 1, Name = "A320", AircraftFamilyId = 1, Family = families[0], FlightRange = 6000, PassengerCapacity = 180, CargoCapacity = 20000 },
        new AircraftModel { Id = 2, Name = "737-800", AircraftFamilyId = 2, Family = families[1], FlightRange = 5500, PassengerCapacity = 160, CargoCapacity = 18000 },
        new AircraftModel { Id = 3, Name = "777-300ER", AircraftFamilyId = 3, Family = families[2], FlightRange = 11000, PassengerCapacity = 370, CargoCapacity = 45000 },
        new AircraftModel { Id = 4, Name = "787-9", AircraftFamilyId = 4, Family = families[3], FlightRange = 12000, PassengerCapacity = 290, CargoCapacity = 40000 },
        new AircraftModel { Id = 5, Name = "A330-300", AircraftFamilyId = 5, Family = families[4], FlightRange = 10500, PassengerCapacity = 300, CargoCapacity = 42000 },
        new AircraftModel { Id = 6, Name = "A350-900", AircraftFamilyId = 6, Family = families[5], FlightRange = 15000, PassengerCapacity = 320, CargoCapacity = 47000 },
        new AircraftModel { Id = 7, Name = "CRJ900", AircraftFamilyId = 7, Family = families[6], FlightRange = 3000, PassengerCapacity = 90, CargoCapacity = 9000 },
        new AircraftModel { Id = 8, Name = "E195", AircraftFamilyId = 8, Family = families[7], FlightRange = 4200, PassengerCapacity = 120, CargoCapacity = 12000 },
        new AircraftModel { Id = 9, Name = "MC-21-300", AircraftFamilyId = 9, Family = families[8], FlightRange = 6000, PassengerCapacity = 210, CargoCapacity = 22000 },
        new AircraftModel { Id = 10, Name = "SSJ100", AircraftFamilyId = 10, Family = families[9], FlightRange = 4500, PassengerCapacity = 100, CargoCapacity = 10000 }
    };

    public List<Flight> GetFlights(List<AircraftModel> models) => new()
    {
        new Flight { Id = 1, Code = "FL001", Departure = "Moscow", Arrival = "Berlin", DepartureDateTime = BaseDate.AddHours(9), ArrivalDateTime = BaseDate.AddHours(11), Duration = TimeSpan.FromHours(2), AircraftModelId = 1, AircraftModel = models[0] },
        new Flight { Id = 2, Code = "FL002", Departure = "Moscow", Arrival = "Paris", DepartureDateTime = BaseDate.AddHours(10), ArrivalDateTime = BaseDate.AddHours(13.5), Duration = TimeSpan.FromHours(3.5), AircraftModelId = 2, AircraftModel = models[1] },
        new Flight { Id = 3, Code = "FL003", Departure = "Berlin", Arrival = "Paris", DepartureDateTime = BaseDate.AddHours(12), ArrivalDateTime = BaseDate.AddHours(13.5), Duration = TimeSpan.FromHours(1.5), AircraftModelId = 3, AircraftModel = models[2] },
        new Flight { Id = 4, Code = "FL004", Departure = "Moscow", Arrival = "Berlin", DepartureDateTime = BaseDate.AddHours(15), ArrivalDateTime = BaseDate.AddHours(17), Duration = TimeSpan.FromHours(2), AircraftModelId = 4, AircraftModel = models[3] },
        new Flight { Id = 5, Code = "FL005", Departure = "Rome", Arrival = "Milan", DepartureDateTime = BaseDate.AddHours(8), ArrivalDateTime = BaseDate.AddHours(9), Duration = TimeSpan.FromHours(1), AircraftModelId = 5, AircraftModel = models[4] },
        new Flight { Id = 6, Code = "FL006", Departure = "Moscow", Arrival = "Tokyo", DepartureDateTime = BaseDate.AddHours(1), ArrivalDateTime = BaseDate.AddHours(12), Duration = TimeSpan.FromHours(11), AircraftModelId = 6, AircraftModel = models[5] },
        new Flight { Id = 7, Code = "FL007", Departure = "Sydney", Arrival = "Auckland", DepartureDateTime = BaseDate.AddHours(6), ArrivalDateTime = BaseDate.AddHours(8.5), Duration = TimeSpan.FromHours(2.5), AircraftModelId = 7, AircraftModel = models[6] },
        new Flight { Id = 8, Code = "FL008", Departure = "New York", Arrival = "London", DepartureDateTime = BaseDate.AddHours(18), ArrivalDateTime = BaseDate.AddHours(24), Duration = TimeSpan.FromHours(6), AircraftModelId = 8, AircraftModel = models[7] },
        new Flight { Id = 9, Code = "FL009", Departure = "Toronto", Arrival = "Montreal", DepartureDateTime = BaseDate.AddHours(19), ArrivalDateTime = BaseDate.AddHours(20), Duration = TimeSpan.FromHours(1), AircraftModelId = 9, AircraftModel = models[8] },
        new Flight { Id = 10, Code = "FL010", Departure = "Shanghai", Arrival = "Beijing", DepartureDateTime = BaseDate.AddHours(7), ArrivalDateTime = BaseDate.AddHours(9), Duration = TimeSpan.FromHours(2), AircraftModelId = 1, AircraftModel = models[0] }
    };

    public List<Passenger> GetPassengers() => new()
    {
        new Passenger { Id = 1, PassportNumber = "P001", FullName = "Alex A", BirthDate = new DateTime(1990,1,1) },
        new Passenger { Id = 2, PassportNumber = "P002", FullName = "Boris B", BirthDate = new DateTime(1985,5,5) },
        new Passenger { Id = 3, PassportNumber = "P003", FullName = "Cid C", BirthDate = new DateTime(1992,3,3) },
        new Passenger { Id = 4, PassportNumber = "P004", FullName = "Dmitry D", BirthDate = new DateTime(1991,4,4) },
        new Passenger { Id = 5, PassportNumber = "P005", FullName = "Ivan I", BirthDate = new DateTime(1988,2,2) },
        new Passenger { Id = 6, PassportNumber = "P006", FullName = "Kirill K", BirthDate = new DateTime(1995,6,6) },
        new Passenger { Id = 7, PassportNumber = "P007", FullName = "Pavel P", BirthDate = new DateTime(1993,7,7) },
        new Passenger { Id = 8, PassportNumber = "P008", FullName = "Sergey S", BirthDate = new DateTime(1987,8,8) },
        new Passenger { Id = 9, PassportNumber = "P009", FullName = "Olga O", BirthDate = new DateTime(1996,9,9) },
        new Passenger { Id = 10, PassportNumber = "P010", FullName = "Maria M", BirthDate = new DateTime(1994,10,10) }
    };

    public List<Ticket> GetTickets(List<Flight> flights, List<Passenger> passengers) => new()
    {
        new Ticket { Id = 1, Flight = flights[0], FlightId = flights[0].Id, Passenger = passengers[0], PassengerId = passengers[0].Id, SeatNumber = "1A", HasHandLuggage = true, BaggageWeight = 20 },
        new Ticket { Id = 2, Flight = flights[0], FlightId = flights[0].Id, Passenger = passengers[1], PassengerId = passengers[1].Id, SeatNumber = "1B", HasHandLuggage = false },
        new Ticket { Id = 3, Flight = flights[0], FlightId = flights[0].Id, Passenger = passengers[2], PassengerId = passengers[2].Id, SeatNumber = "1C", HasHandLuggage = true, BaggageWeight = 5 },
        new Ticket { Id = 4, Flight = flights[0], FlightId = flights[0].Id, Passenger = passengers[3], PassengerId = passengers[3].Id, SeatNumber = "2A", HasHandLuggage = true, BaggageWeight = 15 },
        new Ticket { Id = 5, Flight = flights[0], FlightId = flights[0].Id, Passenger = passengers[4], PassengerId = passengers[4].Id, SeatNumber = "2B", HasHandLuggage = true, BaggageWeight = 10 },
        new Ticket { Id = 6, Flight = flights[0], FlightId = flights[0].Id, Passenger = passengers[5], PassengerId = passengers[5].Id, SeatNumber = "2C", HasHandLuggage = false },

        new Ticket { Id = 7, Flight = flights[1], FlightId = flights[1].Id, Passenger = passengers[0], PassengerId = passengers[0].Id, SeatNumber = "3A", HasHandLuggage = true, BaggageWeight = 12 },
        new Ticket { Id = 8, Flight = flights[1], FlightId = flights[1].Id, Passenger = passengers[1], PassengerId = passengers[1].Id, SeatNumber = "3B", HasHandLuggage = true, BaggageWeight = 8 },
        new Ticket { Id = 9, Flight = flights[1], FlightId = flights[1].Id, Passenger = passengers[2], PassengerId = passengers[2].Id, SeatNumber = "3C", HasHandLuggage = false },
        new Ticket { Id = 10, Flight = flights[1], FlightId = flights[1].Id, Passenger = passengers[3], PassengerId = passengers[3].Id, SeatNumber = "3D", HasHandLuggage = true, BaggageWeight = 5 },

        new Ticket { Id = 11, Flight = flights[2], FlightId = flights[2].Id, Passenger = passengers[4], PassengerId = passengers[4].Id, SeatNumber = "4A", HasHandLuggage = true, BaggageWeight = 8 },
        new Ticket { Id = 12, Flight = flights[2], FlightId = flights[2].Id, Passenger = passengers[5], PassengerId = passengers[5].Id, SeatNumber = "4B", HasHandLuggage = false },
        new Ticket { Id = 13, Flight = flights[2], FlightId = flights[2].Id, Passenger = passengers[6], PassengerId = passengers[6].Id, SeatNumber = "4C", HasHandLuggage = true, BaggageWeight = 10 },
        new Ticket { Id = 14, Flight = flights[2], FlightId = flights[2].Id, Passenger = passengers[7], PassengerId = passengers[7].Id, SeatNumber = "4D", HasHandLuggage = true, BaggageWeight = 5 },
        new Ticket { Id = 15, Flight = flights[2], FlightId = flights[2].Id, Passenger = passengers[8], PassengerId = passengers[8].Id, SeatNumber = "4E", HasHandLuggage = true, BaggageWeight = 7 },

        new Ticket { Id = 16, Flight = flights[3], FlightId = flights[3].Id, Passenger = passengers[0], PassengerId = passengers[0].Id, SeatNumber = "5A", HasHandLuggage = true, BaggageWeight = 10 },
        new Ticket { Id = 17, Flight = flights[3], FlightId = flights[3].Id, Passenger = passengers[1], PassengerId = passengers[1].Id, SeatNumber = "5B", HasHandLuggage = true, BaggageWeight = 5 },
        new Ticket { Id = 18, Flight = flights[3], FlightId = flights[3].Id, Passenger = passengers[2], PassengerId = passengers[2].Id, SeatNumber = "5C", HasHandLuggage = false },

        new Ticket { Id = 19, Flight = flights[4], FlightId = flights[4].Id, Passenger = passengers[3], PassengerId = passengers[3].Id, SeatNumber = "6A", HasHandLuggage = true, BaggageWeight = 6 },
        new Ticket { Id = 20, Flight = flights[4], FlightId = flights[4].Id, Passenger = passengers[4], PassengerId = passengers[4].Id, SeatNumber = "6B", HasHandLuggage = true, BaggageWeight = 4 },

        new Ticket { Id = 21, Flight = flights[5], FlightId = flights[5].Id, Passenger = passengers[5], PassengerId = passengers[5].Id, SeatNumber = "7A", HasHandLuggage = true, BaggageWeight = 12 },

        new Ticket { Id = 22, Flight = flights[6], FlightId = flights[6].Id, Passenger = passengers[6], PassengerId = passengers[6].Id, SeatNumber = "8A", HasHandLuggage = false },

        new Ticket { Id = 23, Flight = flights[7], FlightId = flights[7].Id, Passenger = passengers[7], PassengerId = passengers[7].Id, SeatNumber = "9A", HasHandLuggage = true, BaggageWeight = 7 },

        new Ticket { Id = 24, Flight = flights[8], FlightId = flights[8].Id, Passenger = passengers[8], PassengerId = passengers[8].Id, SeatNumber = "10A", HasHandLuggage = true, BaggageWeight = 6 },

        new Ticket { Id = 25, Flight = flights[9], FlightId = flights[9].Id, Passenger = passengers[9], PassengerId = passengers[9].Id, SeatNumber = "11A", HasHandLuggage = true, BaggageWeight = 5 }
    };
}
