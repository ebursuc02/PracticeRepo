using CinemaTicketsManagement_E17.Application.Results;
using System.ComponentModel;
using System.Diagnostics;

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

    private Cinema(string name) => Name = name;

    public static Result<Cinema> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Cinema>.Fail("Cinema name is required.");

        var cinema = new Cinema(name);
        return Result<Cinema>.Ok(cinema);
    }

    public Result<Movie> AddMovie(Movie movie)
    {
        if (movie is null)
            return Result<Movie>.Fail("The movie is not specified.");

        if (_movies.Contains(movie))
            return Result<Movie>.Fail($"Movie '{movie.Name}' already exists in the cinema.");

        _movies.Add(movie);
        return Result<Movie>.Ok(movie);
    }

    public Result<Room> AddRoom(Room room)
    {
        if(room is  null) 
            return Result<Room>.Fail("The room is not specified.");

        if (_rooms.Contains(room))
            return Result<Room>.Fail($"Room {room.Number} already exists in the cinema.");

        _rooms.Add(room);
        return Result<Room>.Ok(room);
    }

    public Result<Session> AddSession(Session session)
    {
        if (session is null)
            return Result<Session>.Fail("The session is not specified.");

        if (!_rooms.Contains(session.Room)) 
            return Result<Session>.Fail("The session doesn't include a room.");

        if (!_movies.Contains(session.Movie))
            return Result<Session>.Fail("The session doesn't include a movie.");
        
        _sessions.Add(session);
        return Result<Session>.Ok(session);
    }

    public bool Owns(Session session) => _sessions.Contains(session);
}

