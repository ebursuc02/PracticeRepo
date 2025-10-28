namespace CinemaTicketsManagement_E17.Domain.Policies;

public sealed class BasicPricingStrategy : IPricingStrategy
{
    public decimal Compute(Session session, Seat seat)
        => session.Movie.BasePrice
           * seat.Category.PriceModifier
           * session.Format.PriceModifier;
}
