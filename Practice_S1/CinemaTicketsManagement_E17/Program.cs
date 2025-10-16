using CinemaTicketsManagement_E17.Domain;

// Create the cinema
Cinema cinema = new Cinema("Cinema City Iulius Mall, Iasi");

// Define SeatCategories
List<SeatCategory> categories = new List<SeatCategory>
{
    new SeatCategory("front", 0.8),
    new SeatCategory("middle", 1.2),
    new SeatCategory("left"),
    new SeatCategory("right"),
    new SeatCategory("back", 1.3),
    new SeatCategory("VIP", 3)
};

List<ScreenType> formats = new List<ScreenType>
{
    new ScreenType("3D", 1.2),
    new ScreenType("2D"),
    new ScreenType("4D", 2)
};

Movie movie = new Movie("Titanic", 210, 20, formats);

// create Room objects, add Seats and Sessions
Random rand = new Random();
for(int i = 1; i < 6; i++)
{
    var room = new Room(i, cinema);
    cinema.AddRoom(room);
    for( int j = 1; j < 10; j++)
        for( int row = 1; row < 10; row++)
        {
            Seat seat = new Seat(j, row, categories[rand.Next(0, categories.Count)]);
            room.AddSeat(seat);
        }
    var session = new Session(room, movie, new DateTime(2025, 12, 1, 10, 30, 0), formats[rand.Next(0, formats.Count)]);
    cinema.AddSession(session);

}

var wantedSession = cinema.GetSessions()[0];
wantedSession.DisplaySeatsMap();
Console.WriteLine();
var availableSeats = wantedSession.GetAvailableSeats();

cinema.SellTickets(wantedSession, availableSeats[availableSeats.Count / 2]);
cinema.SellTickets(wantedSession, availableSeats[availableSeats.Count - 1]);
cinema.SellTickets(wantedSession, availableSeats[0]);

wantedSession.DisplaySeatsMap();
Console.WriteLine();

Console.WriteLine($"The value earned from {movie.Name} is {movie.GetTotalEarnedMoney():F2}");
