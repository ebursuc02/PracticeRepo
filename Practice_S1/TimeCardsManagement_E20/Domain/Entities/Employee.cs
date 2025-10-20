using TimeCardsManagement_E20.Application.Results;

namespace TimeCardsManagement_E20.Domain.Entities;

public class Employee
{
    private readonly List<AttendanceRecord> _attendanceRecords = new();
    private readonly List<AnnualLeaveRecord> _leaveRecords = new();

    public string Name { get; }
    public string EmployeeID { get; }

    private Employee(string name, string employeeID)
    {
        Name = name;
        EmployeeID = employeeID;
    }

    public static Result<Employee> Create(string? name, string? employeeId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Employee>.Fail("Employee name is required.");

        if (string.IsNullOrWhiteSpace(employeeId))
            return Result<Employee>.Fail("Employee ID is required.");

        var emp = new Employee(name.Trim(), employeeId.Trim());
        return Result<Employee>.Ok(emp);
    }

    public void AddAttendanceRecords(IEnumerable<AttendanceRecord> attendanceRecords)
        => _attendanceRecords.AddRange(attendanceRecords);

    public void AddAnnualLeaveRecords(IEnumerable<AnnualLeaveRecord> leaveRecords)
        => _leaveRecords.AddRange(leaveRecords);

    // Edit by record Id (safe when multiple records exist on same date)
    public Result<AttendanceRecord> EditAttendanceRecord(
        Guid recordId,
        string? projectId = null,
        int? nrHours = null,
        string? task = null,
        WorkLocation? location = null)
    {
        var record = _attendanceRecords.FirstOrDefault(r => r.Id == recordId);
        if (record == null)
            return Result<AttendanceRecord>.Fail($"No attendance record found with id {recordId}.");

        record.Edit(projectId, nrHours, task, location);
        return Result<AttendanceRecord>.Ok(record);
    }

    // Backward-compatible edit by date (if you still need it)
    public Result<AttendanceRecord> EditRecord(
        DateOnly date,
        string? projectId = null,
        int? nrHours = null,
        string? task = null,
        WorkLocation? location = null)
    {
        var record = _attendanceRecords.FirstOrDefault(r => r.Date == date);
        if (record == null)
            return Result<AttendanceRecord>.Fail($"No record found for {date}.");

        record.Edit(projectId, nrHours, task, location);
        return Result<AttendanceRecord>.Ok(record);
    }

    public int GetNrOfHoursForInterval(DateOnly startDate, DateOnly stopDate)
        => _attendanceRecords
            .Where(r => r.Date >= startDate && r.Date <= stopDate)
            .Sum(d => d.Hours);

    public IReadOnlyList<AttendanceRecord> GetAttendanceRecords(DateOnly startDate, DateOnly stopDate)
        => _attendanceRecords.Where(r => r.Date >= startDate && r.Date <= stopDate).ToList();

    public IReadOnlyList<AnnualLeaveRecord> GetLeaveRecords(DateOnly startDate, DateOnly stopDate)
        => _leaveRecords.Where(r => r.Date >= startDate && r.Date <= stopDate).ToList();

    public bool HasAnyRecordsIn(DateOnly start, DateOnly end)
        => _attendanceRecords.Any(r => r.Date >= start && r.Date <= end)
           || _leaveRecords.Any(r => r.Date >= start && r.Date <= end);
}
