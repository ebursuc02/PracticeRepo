namespace CinemaTicketsManagement_E17.Domain;

public class Cinema(string name)
{
    private List<Movie> _movies = [];
    private List<Room> _rooms = [];
    private List<Session> _sessions = [];
    public string Name { get; } = name;

    public void AddMovie(Movie movie) => _movies.Add(movie);
    public void AddRoom(Room room) => _rooms.Add(room);
    public void AddSession(Session session) => _sessions.Add(session);
    public List<Session> GetSessions () => _sessions;

    public void SellTickets(Session session, Seat seat)
    {
        if (!_sessions.Contains(session))
            throw new InvalidOperationException("Session does not belong to this cinema.");

        var room = session.Room ?? throw new InvalidOperationException("Session has no room assigned.");

        if (!room.ContainsSeat(seat))
            throw new ArgumentOutOfRangeException(nameof(seat), $"Seat {seat} does not exist in room {room.Number}.");

        if ( !session.IsSeatSold(seat))
        {
            var price = ComputePrice(session, seat);
            var ticket = new Ticket(seat, price, session);
            session.AddSoldTicket(ticket);
            DisplayTicket(ticket);
        } else
        {
            throw new Exception($"Seat {seat.Number} in row {seat.Row} is not available.");
        }
    }

    private double ComputePrice(Session session, Seat seat) 
        => session.Movie.BasePrice * seat.Category.PriceModifier * session.Format.PriceModifier;

    private void DisplayTicket(Ticket ticket)
    {
        var s = ticket.Session;
        var seat = ticket.Seat;

        Console.WriteLine("========================================");
        Console.WriteLine("               CINEMA TICKET            ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Movie   : {s.Movie.Name}");
        Console.WriteLine($"Room    : {s.Room.Number}");
        Console.WriteLine($"Format  : {s.Format.Name}");
        Console.WriteLine($"When    : {s.StartTime:dd MMM yyyy HH:mm}");
        Console.WriteLine($"Seat    : Row {seat.Row}, Seat {seat.Number} ({seat.Category.Name})");
        Console.WriteLine($"Price   : {ticket.Price:0.00}");
        Console.WriteLine("========================================");
        Console.WriteLine();
    }

}
