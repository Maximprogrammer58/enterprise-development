using AirlineApp.Domain.Entities;

namespace AirlineApp.Tests.Seed;

/// <summary>
/// Seeds test data for the airline system, including aircraft families, models, passengers, flights, and tickets.
/// </summary>
public class DataSeed
{
    private readonly List<AircraftFamily> _aircraftFamilies;
    private readonly List<AircraftModel> _aircraftModels;
    private readonly List<Passenger> _passengers;
    private readonly List<Flight> _flights;
    private readonly List<Ticket> _tickets;

    public DataSeed()
    {
        _aircraftFamilies = InitAircraftFamilies();
        _aircraftModels = InitAircraftModels(_aircraftFamilies);
        _passengers = InitPassengers();
        _flights = InitFlights(_aircraftModels);
        _tickets = InitTickets(_flights, _passengers);
    }

    /// <summary>List of seeded aircraft families.</summary>
    public List<AircraftFamily> AircraftFamilies => _aircraftFamilies;

    /// <summary>List of seeded aircraft models.</summary>
    public List<AircraftModel> AircraftModels => _aircraftModels;

    /// <summary>List of seeded passengers.</summary>
    public List<Passenger> Passengers => _passengers;

    /// <summary>List of seeded flights.</summary>
    public List<Flight> Flights => _flights;

    /// <summary>List of seeded tickets.</summary>
    public List<Ticket> Tickets => _tickets;

    /// <summary>
    /// Initializes the aircraft families with predefined data.
    /// </summary>
    /// <returns>List of <see cref="AircraftFamily"/> objects.</returns>
    private static List<AircraftFamily> InitAircraftFamilies() => [
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
    ];

    /// <summary>
    /// Initializes aircraft models linked to their families.
    /// </summary>
    /// <param name="families">List of aircraft families.</param>
    /// <returns>List of <see cref="AircraftModel"/> objects.</returns>
    private static List<AircraftModel> InitAircraftModels(List<AircraftFamily> families) => [
        new AircraftModel { Id = 1, Name = "A320", Family = families[0], FlightRange = 6000f, PassengerCapacity = 180f, CargoCapacity = 20000f },
        new AircraftModel { Id = 2, Name = "737-800", Family = families[1], FlightRange = 5500f, PassengerCapacity = 160f, CargoCapacity = 18000f },
        new AircraftModel { Id = 3, Name = "777-300ER", Family = families[2], FlightRange = 11000f, PassengerCapacity = 370f, CargoCapacity = 45000f },
        new AircraftModel { Id = 4, Name = "787-9", Family = families[3], FlightRange = 12000f, PassengerCapacity = 290f, CargoCapacity = 40000f },
        new AircraftModel { Id = 5, Name = "A330-300", Family = families[4], FlightRange = 10500f, PassengerCapacity = 300f, CargoCapacity = 42000f },
        new AircraftModel { Id = 6, Name = "A350-900", Family = families[5], FlightRange = 15000f, PassengerCapacity = 320f, CargoCapacity = 47000f },
        new AircraftModel { Id = 7, Name = "CRJ900", Family = families[6], FlightRange = 3000f, PassengerCapacity = 90f, CargoCapacity = 9000f },
        new AircraftModel { Id = 8, Name = "E195", Family = families[7], FlightRange = 4200f, PassengerCapacity = 120f, CargoCapacity = 12000f },
        new AircraftModel { Id = 9, Name = "MC-21-300", Family = families[8], FlightRange = 6000f, PassengerCapacity = 210f, CargoCapacity = 22000f },
        new AircraftModel { Id = 10, Name = "SSJ100", Family = families[9], FlightRange = 4500f, PassengerCapacity = 100f, CargoCapacity = 10000f }
    ];

    /// <summary>
    /// Initializes a list of passengers.
    /// </summary>
    /// <returns>List of <see cref="Passenger"/> objects.</returns>
    private static List<Passenger> InitPassengers() => [
        new Passenger { Id = 1, PassportNumber = "P001", FullName = "Alex A", BirthDate = new DateOnly(1990,1,1) },
        new Passenger { Id = 2, PassportNumber = "P002", FullName = "Boris B", BirthDate = new DateOnly(1985,5,5) },
        new Passenger { Id = 3, PassportNumber = "P003", FullName = "Cid C", BirthDate = new DateOnly(1992,3,3) },
        new Passenger { Id = 4, PassportNumber = "P004", FullName = "Dmitry D", BirthDate = new DateOnly(1991,4,4) },
        new Passenger { Id = 5, PassportNumber = "P005", FullName = "Ivan I", BirthDate = new DateOnly(1988,2,2) },
        new Passenger { Id = 6, PassportNumber = "P006", FullName = "Kirill K", BirthDate = new DateOnly(1995,6,6) },
        new Passenger { Id = 7, PassportNumber = "P007", FullName = "Pavel P", BirthDate = new DateOnly(1993,7,7) },
        new Passenger { Id = 8, PassportNumber = "P008", FullName = "Sergey S", BirthDate = new DateOnly(1987,8,8) },
        new Passenger { Id = 9, PassportNumber = "P009", FullName = "Olga O", BirthDate = new DateOnly(1996,9,9) },
        new Passenger { Id = 10, PassportNumber = "P010", FullName = "Maria M", BirthDate = new DateOnly(1994,10,10) }
    ];

    /// <summary>
    /// Initializes flights with aircraft models and schedules.
    /// </summary>
    /// <param name="models">List of aircraft models.</param>
    /// <returns>List of <see cref="Flight"/> objects.</returns>
    private static List<Flight> InitFlights(List<AircraftModel> models) => [
        new Flight { Id = 1, Code = "FL001", Departure = "Moscow", Arrival = "Berlin", DepartureDateTime = new DateTime(2025, 9, 1, 9, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 11, 0, 0), Duration = TimeSpan.FromHours(2), AircraftModel = models[0] },
        new Flight { Id = 2, Code = "FL002", Departure = "Moscow", Arrival = "Paris", DepartureDateTime = new DateTime(2025, 9, 1, 10, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 13, 30, 0), Duration = TimeSpan.FromHours(3.5), AircraftModel = models[1] },
        new Flight { Id = 3, Code = "FL003", Departure = "Berlin", Arrival = "Paris", DepartureDateTime = new DateTime(2025, 9, 1, 12, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 13, 30, 0), Duration = TimeSpan.FromHours(1.5), AircraftModel = models[2] },
        new Flight { Id = 4, Code = "FL004", Departure = "Moscow", Arrival = "Berlin", DepartureDateTime = new DateTime(2025, 9, 1, 15, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 17, 0, 0), Duration = TimeSpan.FromHours(2), AircraftModel = models[3] },
        new Flight { Id = 5, Code = "FL005", Departure = "Rome", Arrival = "Milan", DepartureDateTime = new DateTime(2025, 9, 1, 8, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 9, 0, 0), Duration = TimeSpan.FromHours(1), AircraftModel = models[4] },
        new Flight { Id = 6, Code = "FL006", Departure = "Moscow", Arrival = "Tokyo", DepartureDateTime = new DateTime(2025, 9, 1, 1, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 12, 0, 0), Duration = TimeSpan.FromHours(11), AircraftModel = models[5] },
        new Flight { Id = 7, Code = "FL007", Departure = "Sydney", Arrival = "Auckland", DepartureDateTime = new DateTime(2025, 9, 1, 6, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 8, 30, 0), Duration = TimeSpan.FromHours(2.5), AircraftModel = models[6] },
        new Flight { Id = 8, Code = "FL008", Departure = "New York", Arrival = "London", DepartureDateTime = new DateTime(2025, 9, 1, 18, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 2, 0, 0, 0), Duration = TimeSpan.FromHours(6), AircraftModel = models[7] },
        new Flight { Id = 9, Code = "FL009", Departure = "Toronto", Arrival = "Montreal", DepartureDateTime = new DateTime(2025, 9, 1, 19, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 20, 0, 0), Duration = TimeSpan.FromHours(1), AircraftModel = models[8] },
        new Flight { Id = 10, Code = "FL010", Departure = "Shanghai", Arrival = "Beijing", DepartureDateTime = new DateTime(2025, 9, 1, 7, 0, 0), ArrivalDateTime = new DateTime(2025, 9, 1, 9, 0, 0), Duration = TimeSpan.FromHours(2), AircraftModel = models[0] }
    ];

    /// <summary>
    /// Initializes tickets linking flights to passengers.
    /// </summary>
    /// <param name="flights">List of flights.</param>
    /// <param name="passengers">List of passengers.</param>
    /// <returns>List of <see cref="Ticket"/> objects.</returns>
    private static List<Ticket> InitTickets(List<Flight> flights, List<Passenger> passengers) => [
        new Ticket { Id = 1, Flight = flights[0], Passenger = passengers[0], SeatNumber = "1A", HasHandLuggage = true, BaggageWeight = 20 },
        new Ticket { Id = 2, Flight = flights[0], Passenger = passengers[1], SeatNumber = "1B", HasHandLuggage = false },
        new Ticket { Id = 3, Flight = flights[0], Passenger = passengers[2], SeatNumber = "1C", HasHandLuggage = true, BaggageWeight = 5 },
        new Ticket { Id = 4, Flight = flights[0], Passenger = passengers[3], SeatNumber = "2A", HasHandLuggage = true, BaggageWeight = 15 },
        new Ticket { Id = 5, Flight = flights[0], Passenger = passengers[4], SeatNumber = "2B", HasHandLuggage = true, BaggageWeight = 10 },
        new Ticket { Id = 6, Flight = flights[0], Passenger = passengers[5], SeatNumber = "2C", HasHandLuggage = false },

        new Ticket { Id = 7, Flight = flights[1], Passenger = passengers[0], SeatNumber = "3A", HasHandLuggage = true, BaggageWeight = 12 },
        new Ticket { Id = 8, Flight = flights[1], Passenger = passengers[1], SeatNumber = "3B", HasHandLuggage = true, BaggageWeight = 8 },
        new Ticket { Id = 9, Flight = flights[1], Passenger = passengers[2], SeatNumber = "3C", HasHandLuggage = false },
        new Ticket { Id = 10, Flight = flights[1], Passenger = passengers[3], SeatNumber = "3D", HasHandLuggage = true, BaggageWeight = 5 },

        new Ticket { Id = 11, Flight = flights[2], Passenger = passengers[4], SeatNumber = "4A", HasHandLuggage = true, BaggageWeight = 8 },
        new Ticket { Id = 12, Flight = flights[2], Passenger = passengers[5], SeatNumber = "4B", HasHandLuggage = false },
        new Ticket { Id = 13, Flight = flights[2], Passenger = passengers[6], SeatNumber = "4C", HasHandLuggage = true, BaggageWeight = 10 },
        new Ticket { Id = 14, Flight = flights[2], Passenger = passengers[7], SeatNumber = "4D", HasHandLuggage = true, BaggageWeight = 5 },
        new Ticket { Id = 15, Flight = flights[2], Passenger = passengers[8], SeatNumber = "4E", HasHandLuggage = true, BaggageWeight = 7 },

        new Ticket { Id = 16, Flight = flights[3], Passenger = passengers[0], SeatNumber = "5A", HasHandLuggage = true, BaggageWeight = 10 },
        new Ticket { Id = 17, Flight = flights[3], Passenger = passengers[1], SeatNumber = "5B", HasHandLuggage = true, BaggageWeight = 5 },
        new Ticket { Id = 18, Flight = flights[3], Passenger = passengers[2], SeatNumber = "5C", HasHandLuggage = false },

        new Ticket { Id = 19, Flight = flights[4], Passenger = passengers[3], SeatNumber = "6A", HasHandLuggage = true, BaggageWeight = 6 },
        new Ticket { Id = 20, Flight = flights[4], Passenger = passengers[4], SeatNumber = "6B", HasHandLuggage = true, BaggageWeight = 4 },

        new Ticket { Id = 21, Flight = flights[5], Passenger = passengers[5], SeatNumber = "7A", HasHandLuggage = true, BaggageWeight = 12 },

        new Ticket { Id = 22, Flight = flights[6], Passenger = passengers[6], SeatNumber = "8A", HasHandLuggage = false },

        new Ticket { Id = 23, Flight = flights[7], Passenger = passengers[7], SeatNumber = "9A", HasHandLuggage = true, BaggageWeight = 7 },

        new Ticket { Id = 24, Flight = flights[8], Passenger = passengers[8], SeatNumber = "10A", HasHandLuggage = true, BaggageWeight = 6 },

        new Ticket { Id = 25, Flight = flights[9], Passenger = passengers[9], SeatNumber = "11A", HasHandLuggage = true, BaggageWeight = 5 }
    ];
}

