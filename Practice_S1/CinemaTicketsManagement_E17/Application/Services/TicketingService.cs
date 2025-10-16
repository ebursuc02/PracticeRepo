using CinemaTicketsManagement_E17.Domain;
using CinemaTicketsManagement_E17.Domain.Abstractions;
using CinemaTicketsManagement_E17.Domain.Policies;

namespace CinemaTicketsManagement_E17.Application.Services;

public class TicketingService(IPricingStrategy pricing, ITicketRender ticketRender)
{
    public Ticket SellTicket(Cinema cinema, Session session, Seat seat)
    {
        if (!cinema.Sessions.Contains(session))
            throw new InvalidOperationException("Session does not belong to this cinema.");

        var price = pricing.Compute(session, seat);
        var ticket = session.IssueTicket(seat, price);

        ticketRender.Render(ticket);
        return ticket;
    }
}
