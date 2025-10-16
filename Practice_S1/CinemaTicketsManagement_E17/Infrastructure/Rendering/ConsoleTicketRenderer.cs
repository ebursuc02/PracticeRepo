using CinemaTicketsManagement_E17.Domain;
using CinemaTicketsManagement_E17.Domain.Abstractions;

namespace CinemaTicketsManagement_E17.Infrastructure.Rendering;

public sealed class ConsoleTicketRenderer : ITicketRender
{
    public void Render(Ticket ticket)
    {
        var s = ticket.Session;
        var seat = ticket.Seat;

        Console.WriteLine("========================================");
        Console.WriteLine("               CINEMA TICKET            ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Movie   : {s.Movie.Name}");
        Console.WriteLine($"Room    : {s.Room.Number}");
        Console.WriteLine($"Format  : {s.Format.Name}");
        Console.WriteLine($"When    : {s.StartTime:dd MMM yyyy HH:mm}");
        Console.WriteLine($"Seat    : Row {seat.Row}, Seat {seat.Number} ({seat.Category.Name})");
        Console.WriteLine($"Price   : {ticket.Price:0.00}");
        Console.WriteLine("========================================");
        Console.WriteLine();
    }
}
