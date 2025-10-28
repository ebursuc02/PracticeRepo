using CurrencyConverter_E9;

Dictionary<string, double> rates  = new Dictionary<string, double>
{
    { "USD", 4.566 },
    { "EUR", 5.08  },
    { "GBP", 5.26 }
};

CurrencyConverter currencyConverter = new CurrencyConverter();
Random random = new Random();

for(int i = 0; i < 30; i++)
    foreach (var currency in rates)
        currencyConverter.AddCurrencyRate(new CurrencyRate(currency.Key, currency.Value + random.NextDouble() * 0.5, DateTime.UtcNow.AddDays(-i)));
        

currencyConverter.Get30DaysReport();