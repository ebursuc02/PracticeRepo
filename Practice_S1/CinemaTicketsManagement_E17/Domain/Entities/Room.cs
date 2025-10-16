namespace CinemaTicketsManagement_E17.Domain;

public class Room
{
    private readonly HashSet<Seat> _seats = [];
    public int Number { get; }
    public IReadOnlyCollection<Seat> Seats => _seats;

    public Room(int number, IEnumerable<Seat> seats = null)
    {
        Number = number;
        _seats = seats != null ? new HashSet<Seat>(seats) : new HashSet<Seat>();
    }
    public void AddSeat(Seat seat) => _seats.Add(seat);
    public bool ContainsSeat(Seat seat) => _seats.Contains(seat);
    public Seat? TryGetSeat(int number, int row) => _seats.FirstOrDefault(s => s.Number == number && s.Row == row);
}
