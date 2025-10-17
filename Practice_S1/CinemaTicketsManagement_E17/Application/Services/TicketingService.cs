using CinemaTicketsManagement_E17.Application.Results;
using CinemaTicketsManagement_E17.Domain;
using CinemaTicketsManagement_E17.Domain.Abstractions;
using CinemaTicketsManagement_E17.Domain.Policies;

namespace CinemaTicketsManagement_E17.Application.Services;

public class TicketingService(IPricingStrategy pricing, ITicketRender ticketRender)
{
    public Result<Ticket> SellTicket(Cinema cinema, Session session, Seat seat)
    {
        if (!cinema.Owns(session))
            return Result<Ticket>.Fail("Session does not belong to this cinema.");

        var price = pricing.Compute(session, seat);

        var issued = session.IssueTicket(seat, price);
        if (!issued.Success)
            return Result<Ticket>.Fail(issued.Error!);

        var ticket = issued.Value!;
        ticketRender.Render(ticket);
        return Result<Ticket>.Ok(ticket);
    }
}
