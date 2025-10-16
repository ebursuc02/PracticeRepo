namespace CinemaTicketsManagement_E17.Domain;

public class Movie
{
    public string Name { get; }
    public int Duration { get; }
    public decimal BasePrice { get; }
    public IReadOnlyList<ScreenType> Formats { get; }

    public Movie(string name, int duration, decimal price, IEnumerable<ScreenType> screenFormats)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));
        Duration = duration;
        BasePrice = price;
        Formats = (screenFormats ?? throw new ArgumentNullException(nameof(screenFormats))).Distinct().ToList();
    }
}
