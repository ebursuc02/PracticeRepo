namespace CinemaTicketsManagement_E17.Domain.Policies;

public interface IPricingStrategy
{
    decimal Compute(Session session, Seat seat);
}
