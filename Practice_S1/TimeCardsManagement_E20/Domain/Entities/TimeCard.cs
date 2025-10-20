using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Application.Results;

namespace TimeCardsManagement_E20.Domain.Entities;

public enum TimeCardStatus { Draft, Approved, Submitted, Rejected }

public sealed class TimeCard
{
    private readonly List<AttendanceRecord> _attendance = new();
    private readonly List<AnnualLeaveRecord> _leave = new();

    public Guid Id { get; } = Guid.NewGuid();
    public string EmployeeId { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public TimeCardStatus Status { get; private set; } = TimeCardStatus.Draft;

    public IReadOnlyList<AttendanceRecord> Attendance => _attendance;
    public IReadOnlyList<AnnualLeaveRecord> Leave => _leave;

    public TimeCard(string employeeId, DateOnly start, DateOnly end)
    {
        if (end < start) throw new ArgumentException("End date must be >= start date.");
        EmployeeId = employeeId;
        PeriodStart = start;
        PeriodEnd = end;
    }

    public Result<AttendanceRecord> AddAttendance(
        IProjectCatalog catalog,
        string projectId,
        string taskCode,
        DateOnly date,
        int hours,
        WorkLocation location)
    {
        if (Status != TimeCardStatus.Draft)
            return Result<AttendanceRecord>.Fail("Cannot modify a non-draft timecard.");

        if (date < PeriodStart || date > PeriodEnd)
            return Result<AttendanceRecord>.Fail("Date is outside the timecard period.");

        var project = catalog.GetById(projectId);
        if (project is null)
            return Result<AttendanceRecord>.Fail($"Unknown project '{projectId}'.");

        if (!catalog.TaskExists(projectId, taskCode))
            return Result<AttendanceRecord>.Fail($"Task '{taskCode}' does not exist for project '{projectId}'.");

        var create = AttendanceRecord.Create(projectId, taskCode, date, hours, location);
        if (!create.Success)
            return create;

        _attendance.Add(create.Value!);
        return create;
    }

    public Result<AnnualLeaveRecord> AddAnnualLeave(DateOnly date)
    {
        if (Status != TimeCardStatus.Draft)
            return Result<AnnualLeaveRecord>.Fail("Cannot modify a non-draft timecard.");

        if (date < PeriodStart || date > PeriodEnd)
            return Result<AnnualLeaveRecord>.Fail("Date is outside the timecard period.");

        var leave = AnnualLeaveRecord.Create(date);
        _leave.Add(leave);
        return Result<AnnualLeaveRecord>.Ok(leave);
    }

    public Result<TimeCard> ApproveIfValid()
    {
        if (Status != TimeCardStatus.Draft)
            return Result<TimeCard>.Fail("Only draft timecards can be approved.");

        if (_leave.Any(l => !l.IsApproved))
            return Result<TimeCard>.Fail("Contains unapproved annual leave.");

        Status = TimeCardStatus.Approved;
        return Result<TimeCard>.Ok(this);
    }

    public Result<TimeCard> Submit()
    {
        if (Status != TimeCardStatus.Approved)
            return Result<TimeCard>.Fail("Timecard must be approved before submission.");

        Status = TimeCardStatus.Submitted;
        return Result<TimeCard>.Ok(this);
    }
}
