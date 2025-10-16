namespace CinemaTicketsManagement_E17.Domain;

public class Movie(string name, int duration, double price, List<ScreenType> screenFormats)
{
    public string Name { get; } = name;
    public int Duration { get; } = duration;
    public double BasePrice { get; } = price;
    public List<ScreenType> Formats { get; } = screenFormats;
    public List<Session> Sessions { get; private set; } = new List<Session>();

    public double GetTotalEarnedMoney() => Sessions.Sum(s => s.GetEarnedMoney());
    public void AddSession(Session session) => Sessions.Add(session);

}
