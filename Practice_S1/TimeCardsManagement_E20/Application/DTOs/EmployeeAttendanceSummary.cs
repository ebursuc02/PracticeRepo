using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Application.DTOs;

public sealed class EmployeeAttendanceSummary
{
    public string Name { get; init; } = "";
    public string EmployeeID { get; init; } = "";
    public IReadOnlyList<AttendanceRecord> Attendance { get; init; } = Array.Empty<AttendanceRecord>();

    public int WorkedDays { get; init; }
    public int OfficeDays { get; init; }
    public int ApprovedLeaveDays { get; init; }
    public int BusinessDays { get; init; }
    public int RequiredOfficeDays { get; init; }

    public double AttendancePctOfWorked =>
        WorkedDays == 0 ? 0.0 : (double)OfficeDays / WorkedDays * 100.0;

    public double CompliancePct =>
        RequiredOfficeDays == 0 ? 100.0 : (double)OfficeDays / RequiredOfficeDays * 100.0;

    public int TotalHours { get; init; }
}
