namespace CurrencyConverter_E9;

public class CurrencyConverter
{
    public List<CurrencyRate> currencyRates { get; } = [];

    public void AddCurrencyRate(CurrencyRate currencyRate) => currencyRates.Add(currencyRate);

    public void Get30DaysReport()
    {
        List<DateTime> days = Enumerable
            .Range(0, 30)
            .Select(i => DateTime.UtcNow.AddDays(-i))
            .ToList();

        Console.WriteLine($"| Date  |  USD  |  EUR  |  GBP  |");

        foreach (var day in days)
        {
            var usdRate = currencyRates.Where(r => r.Date.Date == day.Date && r.Currency == "USD").FirstOrDefault();
            var eurRate = currencyRates.Where(r => r.Date.Date == day.Date && r.Currency == "EUR").FirstOrDefault();
            var gbpRate = currencyRates.Where(r => r.Date.Date == day.Date && r.Currency == "GBP").FirstOrDefault();
            Console.WriteLine($"| {day:dd.MM} | {usdRate?.Rate.ToString("f2") ?? "    "}  | {eurRate?.Rate.ToString("f2") ?? "    "}  | {gbpRate?.Rate.ToString("f2") ?? "    "}  |");
        }
            
    }

}
