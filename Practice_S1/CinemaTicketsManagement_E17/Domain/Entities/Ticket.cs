using CinemaTicketsManagement_E17.Application.Results;

namespace CinemaTicketsManagement_E17.Domain;

public class Ticket
{
    public Session Session { get; }
    public Seat Seat { get; }
    public decimal Price { get; }

    private Ticket(Seat seat, decimal price, Session session)
    {
        Seat = seat;
        Session = session;
        Price = price;
    }

    public static Result<Ticket> Create(Seat seat, decimal price, Session session)
    {
        if (seat is null) return Result<Ticket>.Fail("The seat is not specified.");
        if (session is null) return Result<Ticket>.Fail("The session is not specified.");
        
        var ticket = new Ticket(seat, price, session);
        return Result<Ticket>.Ok(ticket);
    }
}
