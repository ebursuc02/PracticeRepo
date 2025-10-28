using CinemaTicketsManagement_E17.Domain.Abstractions;

namespace CinemaTicketsManagement_E17.Infrastructure.Rendering;

public class ConsoleSeatMapRenderer : ISeatMapRenderer
{
    public void Render(Session session)
    {
        // Header
        Console.WriteLine($"Movie: {session.Movie.Name}");
        Console.WriteLine($"Start Time: {session.StartTime:dd MMM yyyy HH:mm}");
        Console.WriteLine($"Room: {session.Room.Number}");
        Console.WriteLine();
        Console.WriteLine("Legend: A = Available   X = Sold   (blank) = no seat");
        Console.WriteLine();

        // Build presence & sold maps
        var present = new HashSet<(int Row, int Num)>(session.Room.Seats.Select(s => (s.Row, s.Number)));
        var sold = new HashSet<(int Row, int Num)>(session.SoldSeats.Select(s => (s.Row, s.Number)));

        int maxRow = session.Room.Seats.Max(s => s.Row);
        int maxCol = session.Room.Seats.Max(s => s.Number);

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
