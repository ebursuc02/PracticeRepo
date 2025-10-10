namespace YearChecker_E3;

public static class YearChecker
{
    public static void DisplayInfo(int year)
    {
        bool isLeap = DateTime.IsLeapYear(year);
        int days = isLeap ? 366 : 365;
        int weeks = days / 7;

        var holidays = new Dictionary<DateTime, string>
        {
            { new DateTime(year, 12, 25), "Christmas" },
            { new DateTime(year, 1, 1), "New Year" },
            { new DateTime(year, 1, 2), "Second Day of The New Year" },
            { new DateTime(year, 1, 6), "Boboteaza" },
            { new DateTime(year, 6, 1),"Children Day" }
        };

        var workingDays = Enumerable.Range(1, days)
            .Select(d => new DateTime(year, 1, 1).AddDays(d - 1))
            .Count(d => d.DayOfWeek != DayOfWeek.Sunday && d.DayOfWeek != DayOfWeek.Saturday && !holidays.Keys.Contains(d));

        Console.WriteLine($"Leap year: {isLeap}\nNo. Days: {days}\nNo. Working Days: {workingDays}\nNo. Weeks: {weeks}\nHolidays:");
        foreach(var h in holidays) Console.WriteLine($"{h.Value} On {h.Key:dd} of {h.Key:MMMM}");
    }
}
