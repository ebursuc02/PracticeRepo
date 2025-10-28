using CinemaTicketsManagement_E17.Application.Results;
using CinemaTicketsManagement_E17.Domain;

namespace CinemaTicketsManagement_E17.Application.Services;

public class ReportingService
{
    public Result<decimal> CalculateTotalEarnings(Cinema cinema, Movie movie)
    {
        if (cinema is null)
            return Result<decimal>.Fail("Cinema is not specified.");

        if (movie is null)
            return Result<decimal>.Fail("Movie is not specified.");

        var sessions = cinema.Sessions.Where(s => s.Movie == movie).ToList();
        if (sessions.Count == 0)
            return Result<decimal>.Fail("No sessions found for the specified movie in this cinema.");

        var total = sessions.Sum(s => s.GetEarnedMoney());
        return Result<decimal>.Ok(total);
    }
}
