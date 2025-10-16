namespace CinemaTicketsManagement_E17.Domain;

public class Session
{
    private List<Ticket> _soldTickets = [];
    public Room Room { get; }
    public Movie Movie { get; }
    public DateTime StartTime { get; }
    public ScreenType Format { get; }

    public Session(Room room, Movie movie, DateTime startTime, ScreenType format)
    {
        Room = room;
        Movie = movie;
        StartTime = startTime;
        Format = format;

        movie.AddSession(this);
    }

    public bool IsSeatSold(Seat seat) => _soldTickets.Any(t => t.Seat == seat);
    public void AddSoldTicket(Ticket ticket) => _soldTickets.Add(ticket);
    public double GetEarnedMoney() => _soldTickets.Sum(t => t.Price);
    public List<Seat> GetAvailableSeats() => Room.Seats
        .Where(s => !_soldTickets.Any(t => t.Seat.Equals(s)))
        .ToList();
    public void DisplaySeatsMap()
    {
        // Header
        Console.WriteLine($"Movie: {Movie.Name}");
        Console.WriteLine($"Start Time: {StartTime:dd MMM yyyy HH:mm}");
        Console.WriteLine($"Room: {Room.Number}");
        Console.WriteLine();
        Console.WriteLine("Legend: A = Available   X = Sold   (blank) = no seat");
        Console.WriteLine();

        // Build presence & sold maps
        var present = new HashSet<(int Row, int Num)>(Room.Seats.Select(s => (s.Row, s.Number)));
        var sold = new HashSet<(int Row, int Num)>(_soldTickets.Select(t => (t.Seat.Row, t.Seat.Number)));

        int maxRow = Room.Seats.Max(s => s.Row);
        int maxCol = Room.Seats.Max(s => s.Number);

        int labelW = maxRow.ToString().Length + 2; // row labels at left
        int cellW = 3;                             // width of each seat cell
        string sep = " ";                          // spacing between cells

        var old = Console.ForegroundColor;

        // Rows with left labels
        for (int r = 1; r <= maxRow; r++)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(r.ToString().PadLeft(labelW)); // row label
            for (int c = 1; c <= maxCol; c++)
            {
                char ch = present.Contains((r, c))
                            ? (sold.Contains((r, c)) ? 'X' : 'A')
                            : ' ';

                // simple coloring for flair
                if (ch == 'A') Console.ForegroundColor = ConsoleColor.Green;
                else if (ch == 'X') Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = old;

                Console.Write($" {ch} ".PadRight(cellW));
                Console.Write(sep);
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = old;

        // Bottom seat numbers
        Console.Write(new string(' ', labelW));
        Console.ForegroundColor = ConsoleColor.White;
        for (int c = 1; c <= maxCol; c++)
        {
            string label = c.ToString().PadLeft(2).PadRight(cellW);
            Console.Write(label);
            Console.Write(sep);
        }
        Console.WriteLine();
    }
}

