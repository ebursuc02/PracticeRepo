namespace CinemaTicketsManagement_E17.Domain;

public class Ticket
{
    public Session Session { get; }
    public Seat Seat { get; }
    public decimal Price { get; }

    public Ticket(Seat seat, decimal price, Session session)
    {
        Seat = seat ?? throw new ArgumentNullException(nameof(seat));
        Session = session ?? throw new ArgumentNullException(nameof(session));
        Price = price;
    }
}
