namespace CinemaTicketsManagement_E17.Domain;

public class ScreenType(string name, double modifier = 1)
{
    public string Name { get; } = name;
    public double PriceModifier { get; } = modifier;
}
