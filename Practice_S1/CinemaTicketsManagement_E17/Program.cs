using CinemaTicketsManagement_E17.Application.Services;
using CinemaTicketsManagement_E17.Domain;
using CinemaTicketsManagement_E17.Domain.Policies;
using CinemaTicketsManagement_E17.Infrastructure.Rendering;

//
// ----------------- Scenario entrypoint -----------------
//
var cinema = SeedCinema("Cinema City Iulius Mall, Iasi",
    rooms: 5, seatsPerRow: 9, rowsPerRoom: 9, seed: 42);

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

static Cinema SeedCinema(string cinemaName, int rooms, int seatsPerRow, int rowsPerRoom, int seed)
{
    var rnd = new Random(seed);

    var cinema = new Cinema(cinemaName);

    var categories = CreateSeatCategories();
    var formats = CreateScreenFormats();

    var titanic = new Movie("Titanic", duration: 210, price: 20m, screenFormats: formats);
    cinema.AddMovie(titanic);

    for (int roomNo = 1; roomNo <= rooms; roomNo++)
    {
        var room = new Room(roomNo);
        cinema.AddRoom(room);

        // seats
        for (int row = 1; row <= rowsPerRoom; row++)
        {
            for (int seatNo = 1; seatNo <= seatsPerRow; seatNo++)
            {
                var category = categories[rnd.Next(categories.Count)];
                room.AddSeat(new Seat(seatNo, row, category));
            }
        }

        // one session per room, with a random format allowed by the movie
        var format = formats[rnd.Next(formats.Count)];
        var start = new DateTime(2025, 12, 1, 10, 30, 0);
        var session = new Session(room, titanic, start, format);
        cinema.AddSession(session);
    }

    return cinema;
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
