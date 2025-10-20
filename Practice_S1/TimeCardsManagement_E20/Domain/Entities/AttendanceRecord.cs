using TimeCardsManagement_E20.Application.Results;

namespace TimeCardsManagement_E20.Domain.Entities;

public sealed class AttendanceRecord : TimeRecord
{
    public string ProjectId { get; private set; }
    public string Task { get; private set; }
    public int Hours { get; private set; }
    public WorkLocation Location { get; private set; }

    private AttendanceRecord(string projectId, string task, DateOnly date, int hours, WorkLocation location)
        : base(date)
    {
        ProjectId = projectId;
        Task = task;
        Hours = hours;
        Location = location;
    }

    public static Result<AttendanceRecord> Create(
        string projectId,
        string task,
        DateOnly date,
        int hours,
        WorkLocation location)
    {
        if (string.IsNullOrWhiteSpace(projectId))
            return Result<AttendanceRecord>.Fail("Project ID is required.");

        if (string.IsNullOrWhiteSpace(task))
            return Result<AttendanceRecord>.Fail("Task name is required.");

        if (hours <= 0)
            return Result<AttendanceRecord>.Fail("Number of hours must be greater than zero.");

        var record = new AttendanceRecord(projectId.Trim(), task.Trim(), date, hours, location);
        return Result<AttendanceRecord>.Ok(record);
    }

    public void Edit(string? projectId = null, int? nrHours = null, string? task = null, WorkLocation? location = null)
    {
        ProjectId = projectId ?? ProjectId;
        Task = task ?? Task;
        Hours = nrHours ?? Hours;
        Location = location ?? Location;
    }

    public override string ToString() =>
        $"{Date:yyyy-MM-dd} | {ProjectId} | {Task} | {Location} | {Hours}h";
}
