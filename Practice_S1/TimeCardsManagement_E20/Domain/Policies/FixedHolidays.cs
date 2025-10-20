using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Domain.Policies;

public sealed class FixedHolidays : IHolidayPolicy
{
    private readonly HashSet<DateOnly> _holidays;
    public FixedHolidays(IEnumerable<DateOnly> holidays) => _holidays = [.. holidays];

    public bool IsHoliday(DateOnly d, Employee _)
        => d.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || _holidays.Contains(d);
}
