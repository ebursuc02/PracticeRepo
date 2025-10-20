namespace TimeCardsManagement_E20.Application.DTOs;

public sealed class AttendanceReport
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public List<EmployeeAttendanceSummary> Employees { get; } = new();
    public int TotalRequiredOfficeDays { get; set; }
    public int TotalActualOfficeDays { get; set; }

    public double OverallCompliancePct =>
        TotalRequiredOfficeDays == 0 ? 100.0
                                     : (double)TotalActualOfficeDays / TotalRequiredOfficeDays * 100.0;
}
