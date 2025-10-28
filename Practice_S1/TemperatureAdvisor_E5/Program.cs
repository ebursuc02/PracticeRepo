using System.Text.Json;

Console.Write("Enter the wanted location (recommended <country>, <city>): ");
string location = Console.ReadLine();

Console.Write("Enter the temperature: ");
double temperature;
while( !double.TryParse(Console.ReadLine(), out temperature))
{
    Console.Write("Enter a valid temperature: ");
}

using var httpClient = new HttpClient();

// Required by Nominatim – always include a User-Agent header
httpClient.DefaultRequestHeaders.Add("User-Agent", "CSharpApp/1.0");

// step 1: get coordinates for the country
string geoUrl = $"https://nominatim.openstreetmap.org/search?q={location}&format=json&limit=1";
var geoResponse = await httpClient.GetStringAsync(geoUrl);

var geoData = JsonSerializer.Deserialize<JsonElement>(geoResponse);

if (geoData.GetArrayLength() == 0)
{
    Console.WriteLine("Location not found.");
    return;
}

var lat = geoData[0].GetProperty("lat").GetString();
var lon = geoData[0].GetProperty("lon").GetString();

//Console.WriteLine($"Coordinates for {location}: Lat={lat}, Lon={lon}");

// step 2: get temperature mean for today using Open-Meteo
string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
string weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&daily=temperature_2m_mean&start_date={date}&end_date={date}&timezone=auto";

var weatherResponse = await httpClient.GetStringAsync(weatherUrl);

var weatherData = JsonSerializer.Deserialize<JsonElement>(weatherResponse);
var tempMean = double.Parse(weatherData.GetProperty("daily").GetProperty("temperature_2m_mean")[0].ToString());

Console.WriteLine($"Temperature mean on {date} in {location}: {tempMean}C.");

string result = "Average";

if (temperature < tempMean - 1.5) result = "Cold";
else if (temperature > tempMean + 5) result = "Very Hot";
else if (temperature > tempMean + 1.5) result = "Hot";

Console.WriteLine($"Verdict: {result}");