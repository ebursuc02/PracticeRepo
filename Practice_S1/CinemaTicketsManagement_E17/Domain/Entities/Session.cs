using CinemaTicketsManagement_E17.Application.Results;
using CinemaTicketsManagement_E17.Domain;
using System.Reflection.Metadata.Ecma335;

public class Session
{
    private readonly List<Ticket> _soldTickets = new();
    private readonly HashSet<Seat> _soldSeats = new();

    public Room Room { get; }
    public Movie Movie { get; }
    public DateTimeOffset StartTime { get; }
    public ScreenType Format { get; }
    public IReadOnlyCollection<Seat> SoldSeats => _soldSeats;
    private Session(Room room, Movie movie, DateTimeOffset startTime, ScreenType format)
    {
        Room = room;
        Movie = movie;
        StartTime = startTime;
        Format = format;
    }

    public static Result<Session> Create(Room room, Movie movie, DateTimeOffset startTime, ScreenType format)
    {
        if (room is null)
            return Result<Session>.Fail("The session room is not specified.");

        if (movie is null)
            return Result<Session>.Fail("The session movie is not specified.");

        if (format is null)
            return Result<Session>.Fail("The session format is not specified.");

        if (!movie.Formats.Contains(format))
            return Result<Session>.Fail($"Movie '{movie.Name}' does not support format '{format.Name}'.");

        var session = new Session(room, movie, startTime, format);
        return Result<Session>.Ok(session);
    }

    public bool IsSeatSold(Seat seat) => _soldSeats.Contains(seat);

    public IReadOnlyList<Seat> GetAvailableSeats()
        => Room.Seats.Where(s => !_soldSeats.Contains(s)).ToList();

    public Result<Ticket> IssueTicket(Seat seat, decimal price)
    {
        if (seat is null)
            return Result<Ticket>.Fail("There is no seat specified.");

        if (!Room.ContainsSeat(seat))
            return Result<Ticket>.Fail($"Seat {seat.Row}-{seat.Number} not in room {Room.Number}.");

        if (_soldSeats.Contains(seat))
            return Result<Ticket>.Fail($"Seat {seat.Number}. row {seat.Row} is occupied.");

        var ticketRes = Ticket.Create(seat, price, this);
        if (!ticketRes.Success)
            return Result<Ticket>.Fail($"Ticket creation failed: {ticketRes.Error}");

        var ticket = ticketRes.Value!;
        _soldTickets.Add(ticket);
        _soldSeats.Add(seat);
        return Result<Ticket>.Ok(ticket);
    }

    public decimal GetEarnedMoney() => _soldTickets.Sum(t => t.Price);
}
