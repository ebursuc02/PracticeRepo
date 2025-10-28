namespace CurrencyConverter_E9;

public class CurrencyRate
{
    public string Currency { get; }
    public double Rate { get; }
    public DateTime Date { get; }

    public CurrencyRate(string currency, double rate, DateTime date)
    {
        Currency = currency;
        Rate = rate;
        Date = date;
    }
}
