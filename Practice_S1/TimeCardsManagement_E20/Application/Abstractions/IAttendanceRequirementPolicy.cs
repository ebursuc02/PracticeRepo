using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Application.Abstractions;

public interface IAttendanceRequirementPolicy
{
    int ComputeRequiredOfficeDays(int businessDaysInInterval, Employee employee);
}
