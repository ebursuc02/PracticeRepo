using CinemaTicketsManagement_E17.Domain;

public class Session
{
    private readonly List<Ticket> _soldTickets = new();
    private readonly HashSet<Seat> _soldSeats = new();

    public Room Room { get; }
    public Movie Movie { get; }
    public DateTimeOffset StartTime { get; }
    public ScreenType Format { get; }
    public IReadOnlyCollection<Seat> SoldSeats => _soldSeats;
    public Session(Room room, Movie movie, DateTimeOffset startTime, ScreenType format)
    {
        Room = room ?? throw new ArgumentNullException(nameof(room));
        Movie = movie ?? throw new ArgumentNullException(nameof(movie));
        StartTime = startTime;
        Format = format ?? throw new ArgumentNullException(nameof(format));

        if (!Movie.Formats.Contains(format))
            throw new InvalidOperationException($"Movie '{Movie.Name}' does not support format '{format.Name}'.");
    }

    public bool IsSeatSold(Seat seat) => _soldSeats.Contains(seat);

    public IReadOnlyList<Seat> GetAvailableSeats()
        => Room.Seats.Where(s => !_soldSeats.Contains(s)).ToList();

    public Ticket IssueTicket(Seat seat, decimal price)
    {
        ArgumentNullException.ThrowIfNull(seat);
        if (!Room.ContainsSeat(seat))
            throw new ArgumentOutOfRangeException(nameof(seat), $"Seat {seat.Row}-{seat.Number} not in room {Room.Number}.");
        if (_soldSeats.Contains(seat))
            throw new Exception($"Seat {seat.Number}. row {seat.Row} is occupied.");

        var ticket = new Ticket(seat, price, this);
        _soldTickets.Add(ticket);
        _soldSeats.Add(seat);
        return ticket;
    }

    public decimal GetEarnedMoney() => _soldTickets.Sum(t => t.Price);
}
