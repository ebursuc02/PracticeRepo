namespace CinemaTicketsManagement_E17.Domain;
public sealed class Cinema
{
    private readonly List<Movie> _movies = new();
    private readonly List<Room> _rooms = new();
    private readonly List<Session> _sessions = new();

    public string Name { get; }

    public IReadOnlyList<Movie> Movies => _movies;
    public IReadOnlyList<Room> Rooms => _rooms;
    public IReadOnlyList<Session> Sessions => _sessions;

    public Cinema(string name)
        => Name = name ?? throw new ArgumentNullException(nameof(name));

    public void AddMovie(Movie movie)
    {
        ArgumentNullException.ThrowIfNull(movie);
        if (!_movies.Contains(movie)) _movies.Add(movie);
    }

    public void AddRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);
        if (!_rooms.Contains(room)) _rooms.Add(room);
    }

    public void AddSession(Session session)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (!_rooms.Contains(session.Room)) throw new InvalidOperationException("Room not owned by this cinema.");
        if (!_movies.Contains(session.Movie)) throw new InvalidOperationException($"Movie not owned by this cinema.");
        _sessions.Add(session);
    }

    public bool Owns(Session session) => _sessions.Contains(session);
}

