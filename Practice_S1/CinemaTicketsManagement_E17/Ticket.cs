namespace CinemaTicketsManagement_E17;

public class Ticket
{
    public string SeatCategory { get; }
    public double BasePrice {  get; }
    public string ScreenType { get; }

    public Ticket(string seatCategory, double basePrice, string screenType)
    {
        SeatCategory = seatCategory;
        BasePrice = basePrice;
        ScreenType = screenType;
    }

    public double GetPrice()
    {
        double multiplierBasedOnSeat = SeatCategory switch
        {
            "front" => 0.8,
            "middle" => 1.0,
            "back" => 1.2,
            _ => 1.0
        };

        double multiplierBasedOnScreen = ScreenType switch
        {
            "3D" => 1.2,
            "4D" => 2.0,
            _ => 1.0
        };

        return BasePrice * multiplierBasedOnScreen * multiplierBasedOnSeat;
    }
}
