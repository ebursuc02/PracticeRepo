using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Application.Abstractions;

public interface IHolidayPolicy
{
    bool IsHoliday(DateOnly date, Employee employee);
}
