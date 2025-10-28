using CinemaTicketsManagement_E17.Application.Results;
using CinemaTicketsManagement_E17.Application.Services;
using CinemaTicketsManagement_E17.Domain;
using CinemaTicketsManagement_E17.Domain.Policies;
using CinemaTicketsManagement_E17.Infrastructure.Rendering;

//
// ----------------- Scenario entrypoint -----------------
//
var cinemaRes = SeedCinema("Cinema City Iulius Mall, Iasi",
    rooms: 5, seatsPerRow: 9, rowsPerRoom: 9, seed: 42);
if (!cinemaRes.Success)
{
    Console.WriteLine("Error: " + cinemaRes.Error);
    return;
}
var cinema = cinemaRes.Value!;

var seatMapRenderer = new ConsoleSeatMapRenderer();
var pricing = new BasicPricingStrategy();
var ticketRenderer = new ConsoleTicketRenderer();
var ticketing = new TicketingService(pricing, ticketRenderer);

// Choose a session we’ll play with
var session = cinema.Sessions[0];

Console.WriteLine("Initial seat map:");
seatMapRenderer.Render(session);

// Sell a few seats
var available = session.GetAvailableSeats();
SellSomeSeats(ticketing, cinema, session, available);

// Show seat map after sales
Console.WriteLine();
Console.WriteLine("After selling a few seats:");
seatMapRenderer.Render(session);

// Compute earnings for this movie at the cinema level
var totalForMovie = cinema.Sessions
    .Where(s => s.Movie == session.Movie)
    .Sum(s => s.GetEarnedMoney());
Console.WriteLine();
Console.WriteLine($"Total earned for '{session.Movie.Name}' (this cinema): {totalForMovie:F2}");


//
// ----------------- Helpers -----------------
//

static Result<Cinema> SeedCinema(
    string cinemaName,
    int rooms,
    int seatsPerRow,
    int rowsPerRoom,
    int seed)
{
    var rnd = new Random(seed);

    var cinemaRes = Cinema.Create(cinemaName);
    if (!cinemaRes.Success)
        return Result<Cinema>.Fail($"Cinema creation failed: {cinemaRes.Error}");
    var cinema = cinemaRes.Value!;

    var categories = CreateSeatCategories();
    var formats = CreateScreenFormats();

    var titanicRes = Movie.Create("Titanic", duration: 210, price: 20m, screenFormats: formats);
    if (!titanicRes.Success)
        return Result<Cinema>.Fail($"Movie creation failed: {titanicRes.Error}");
    var titanic = titanicRes.Value!;

    var addMovieRes = cinema.AddMovie(titanic);
    if (!addMovieRes.Success)
        return Result<Cinema>.Fail($"AddMovie failed: {addMovieRes.Error}");

    for (int roomNo = 1; roomNo <= rooms; roomNo++)
    {
        var room = new Room(roomNo);
        var addRoomRes = cinema.AddRoom(room);
        if (!addRoomRes.Success)
            return Result<Cinema>.Fail($"AddRoom({roomNo}) failed: {addRoomRes.Error}");

        // seats
        for (int row = 1; row <= rowsPerRoom; row++)
        {
            for (int seatNo = 1; seatNo <= seatsPerRow; seatNo++)
            {
                var category = categories[rnd.Next(categories.Count)];
                room.AddSeat(new Seat(seatNo, row, category));
            }
        }

        var format = formats[rnd.Next(formats.Count)];
        var start = new DateTime(2025, 12, 1, 10, 30, 0);

        var sessionRes = Session.Create(room, titanic, start, format);
        if (!sessionRes.Success)
            return Result<Cinema>.Fail($"Session creation failed in room {roomNo}: {sessionRes.Error}");

        var addSessionRes = cinema.AddSession(sessionRes.Value!);
        if (!addSessionRes.Success)
            return Result<Cinema>.Fail($"AddSession failed in room {roomNo}: {addSessionRes.Error}");
    }

    return Result<Cinema>.Ok(cinema);
}

static List<SeatCategory> CreateSeatCategories() => new()
{
    new SeatCategory("front", 0.8m),
    new SeatCategory("middle", 1.2m),
    new SeatCategory("left", 1m),
    new SeatCategory("right", 1m),
    new SeatCategory("back", 1.3m),
    new SeatCategory("VIP", 3m)
};

static List<ScreenType> CreateScreenFormats() => new()
{
    new ScreenType("3D", 1.2m),
    new ScreenType("2D", 1.0m),
    new ScreenType("4D", 2.0m)
};

static void SellSomeSeats(
    TicketingService ticketing,
    Cinema cinema,
    Session session,
    IReadOnlyList<Seat> available)
{
    if (available.Count == 0) return;

    // pick three reasonably different seats (start, middle, end)
    var mid = available[available.Count / 2];
    var last = available[^1];
    var first = available[0];

    ticketing.SellTicket(cinema, session, mid);
    ticketing.SellTicket(cinema, session, last);
    ticketing.SellTicket(cinema, session, first);
}
