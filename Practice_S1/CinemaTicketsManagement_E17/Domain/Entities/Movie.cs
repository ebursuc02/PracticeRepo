using CinemaTicketsManagement_E17.Application.Results;

namespace CinemaTicketsManagement_E17.Domain;

public class Movie
{
    public string Name { get; }
    public int Duration { get; }
    public decimal BasePrice { get; }
    public IReadOnlyList<ScreenType> Formats { get; }

    private Movie(string name, int duration, decimal price, IEnumerable<ScreenType> screenFormats)
    {
        Name = name;
        Duration = duration;
        BasePrice = price;
        Formats = screenFormats.Distinct().ToList();
    }

    public static Result<Movie> Create(string name, int duration, decimal price, IEnumerable<ScreenType> screenFormats)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Movie>.Fail("Movie name is required.");

        if (duration <= 0)
            return Result<Movie>.Fail("Movie duration must be positive.");

        if (price < 0)
            return Result<Movie>.Fail("Price cannot be negative.");

        if (screenFormats is null || !screenFormats.Any())
            return Result<Movie>.Fail("At least one screen format must be provided.");

        var movie = new Movie(name, duration, price, screenFormats);
        return Result<Movie>.Ok(movie);
    }
}
