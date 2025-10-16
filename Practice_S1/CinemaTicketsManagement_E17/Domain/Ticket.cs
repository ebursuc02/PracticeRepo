namespace CinemaTicketsManagement_E17.Domain;

public class Ticket(Seat seat, double price, Session session)
{
    public Session Session { get; } = session;
    public Seat Seat { get; } = seat;
    public double Price { get; } = price;
}
