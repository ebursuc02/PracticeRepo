namespace CinemaTicketsManagement_E17.Domain;

public class Room(int number, Cinema cinema)
{
    public HashSet<Seat> Seats { get; } = [];
    public int Number { get; } = number;
    public Cinema Cinema { get; } = cinema;
    
    public void AddSeat(Seat seat) => Seats.Add(seat);
    public bool ContainsSeat(Seat seat) => Seats.Contains(seat);
    public Seat GetSeat(int number, int row) => Seats.First(s => s.Number == number && s.Row == row);
}
