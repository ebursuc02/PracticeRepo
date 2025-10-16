namespace CinemaTicketsManagement_E17.Domain;

public class Seat(int number, int row, SeatCategory category)
{
    public int Number { get; } = number;
    public int Row { get; } = row;
    public SeatCategory Category { get; } = category;
}
