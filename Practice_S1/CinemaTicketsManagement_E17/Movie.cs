namespace CinemaTicketsManagement_E17;

public class Movie
{
    public string Name { get; }
    private List<Ticket> _tickets = [];

    public Movie(string name) => Name = name;

    public void SellTickets(int count, string screenType, string seatCategory, double price)
    {
        for (int i = 0; i < count; i++)
            _tickets.Add(new Ticket(seatCategory, price, screenType));
    }

    public double GetTotalEarnedMoney() => _tickets.Sum(t => t.GetPrice());

}
