using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Domain.Policies;

public sealed class FixedAttendanceRequirement : IAttendanceRequirementPolicy
{
    private readonly int _required;
    public FixedAttendanceRequirement(int required) => _required = Math.Max(0, required);

    public int ComputeRequiredOfficeDays(int businessDaysInInterval, Employee _)
        => Math.Min(_required, Math.Max(0, businessDaysInInterval));
}