using CinemaTicketsManagement_E17.Domain;

namespace CinemaTicketsManagement_E17.Application.Services;

public class ReportingService
{
    public decimal CalculateTotalEarnings(Cinema cinema, Movie movie)
    {
        ArgumentNullException.ThrowIfNull(cinema);
        ArgumentNullException.ThrowIfNull(movie);
        return cinema.Sessions
            .Where(s => s.Movie == movie)
            .Sum(s => s.GetEarnedMoney());
    }
}
