namespace CinemaTicketsManagement_E17.Domain;

public class SeatCategory(string name, double priceModifier = 1)
{
    public string Name { get; } = name; 
    public double PriceModifier { get; } = priceModifier;
}
