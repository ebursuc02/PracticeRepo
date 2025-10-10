using YearChecker_E3;

Console.WriteLine("Enter a year (or enter to exit):");
string year = Console.ReadLine();

while( !string.IsNullOrWhiteSpace(year) )
{
    if (int.TryParse(year, out int parsedYear))  YearChecker.DisplayInfo(parsedYear);
    else Console.WriteLine("The year is not valid.");

    Console.WriteLine("\nEnter a year (or enter to exit):");
    year = Console.ReadLine();
}
    

