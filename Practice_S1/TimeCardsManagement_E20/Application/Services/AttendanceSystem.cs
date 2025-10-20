using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Application.Results;
using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Application.Services;

public sealed class AttendanceSystem
{
    private readonly List<Employee> _employees = new();
    private readonly List<TimeCard> _timeCards = new();
    private readonly IAttendanceRequirementPolicy _requirementPolicy;
    private readonly IHolidayPolicy _holidayPolicy;
    private readonly IProjectCatalog _projectCatalog;

    public AttendanceSystem(
        IAttendanceRequirementPolicy requirementPolicy,
        IHolidayPolicy holidayPolicy,
        IProjectCatalog projectCatalog)
    {
        _requirementPolicy = requirementPolicy;
        _holidayPolicy = holidayPolicy;
        _projectCatalog = projectCatalog;
    }

    // Employee and Catalog actions

    public void AddEmployee(Employee employee) => _employees.Add(employee);

    public IReadOnlyList<Project> ListProjects() => _projectCatalog.GetProjects().ToList();

    public IReadOnlyList<ProjectTask> ListTasksFor(string projectId)
        => _projectCatalog.GetTasksFor(projectId).ToList();

    // TimeCard actions

    public Result<TimeCard> CreateTimeCard(string employeeId, DateOnly start, DateOnly end)
    {
        if (_employees.All(e => e.EmployeeID != employeeId))
            return Result<TimeCard>.Fail($"Unknown employee '{employeeId}'.");

        var tc = new TimeCard(employeeId, start, end);
        _timeCards.Add(tc);
        return Result<TimeCard>.Ok(tc);
    }

    public Result<AttendanceRecord> AddAttendanceToCard(
        Guid timeCardId,
        string projectId,
        string taskCode,
        DateOnly date,
        int hours,
        WorkLocation location)
    {
        var card = _timeCards.FirstOrDefault(c => c.Id == timeCardId);
        if (card is null) return Result<AttendanceRecord>.Fail("Timecard not found.");

        return card.AddAttendance(_projectCatalog, projectId, taskCode, date, hours, location);
    }

    public Result<AnnualLeaveRecord> AddLeaveToCard(Guid timeCardId, DateOnly date)
    {
        var card = _timeCards.FirstOrDefault(c => c.Id == timeCardId);
        if (card is null) return Result<AnnualLeaveRecord>.Fail("Timecard not found.");
        return card.AddAnnualLeave(date);
    }

    public Result<TimeCard> ApproveTimeCard(Guid timeCardId)
    {
        var card = _timeCards.FirstOrDefault(c => c.Id == timeCardId);
        if (card is null) return Result<TimeCard>.Fail("Timecard not found.");
        return card.ApproveIfValid();
    }

    public Result<TimeCard> SubmitTimeCard(Guid timeCardId)
    {
        var card = _timeCards.FirstOrDefault(c => c.Id == timeCardId);
        if (card is null) return Result<TimeCard>.Fail("Timecard not found.");
        return card.Submit();
    }

    // Report

    public void GetReportForInterval(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
        {
            Console.WriteLine("Invalid date interval.");
            return;
        }

        Console.WriteLine($"=== ATTENDANCE REPORT [{startDate:yyyy-MM-dd} .. {endDate:yyyy-MM-dd}] ===");

        int totalRequiredOfficeDays = 0;
        int totalActualOfficeDays = 0;

        foreach (var emp in _employees)
        {
            var attendance = emp.GetAttendanceRecords(startDate, endDate);
            var leaves = emp.GetLeaveRecords(startDate, endDate);
            var approvedLeaveDays = leaves.Count(l => l.IsApproved);

            int workedDays = attendance
                .Select(r => r.Date)
                .Distinct()
                .Count();

            int officeDays = attendance
                .Where(r => r.Location == WorkLocation.Office)
                .Select(r => r.Date)
                .Distinct()
                .Count();

            int businessDays = EnumerateDays(startDate, endDate)
                .Count(d => !_holidayPolicy.IsHoliday(d, emp));

            int businessMinusLeave = Math.Max(0, businessDays - approvedLeaveDays);
            int requiredOfficeDays = _requirementPolicy.ComputeRequiredOfficeDays(businessMinusLeave, emp);

            totalRequiredOfficeDays += requiredOfficeDays;
            totalActualOfficeDays += officeDays;

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"{emp.Name} ({emp.EmployeeID})");
            foreach (var r in attendance.OrderBy(r => r.Date))
                Console.WriteLine($"{r.Date:yyyy-MM-dd} | {r.ProjectId} | {r.Task} | {r.Location} | {r.Hours}h");

            Console.WriteLine($"Worked days: {workedDays}");
            Console.WriteLine($"Office days: {officeDays}");
            Console.WriteLine($"Approved leave days: {approvedLeaveDays}");
            Console.WriteLine($"Business days (excl. weekends/holidays): {businessDays}");
            Console.WriteLine($"Required office days (policy): {requiredOfficeDays}");

            var pctOfWorked = workedDays == 0 ? 0.0 : (double)officeDays / workedDays * 100.0;
            var pctOfRequired = requiredOfficeDays == 0 ? 100.0 : (double)officeDays / requiredOfficeDays * 100.0;

            Console.WriteLine($"Attendance % of worked days: {pctOfWorked:F2}%");
            Console.WriteLine($"Compliance vs required: {officeDays}/{requiredOfficeDays} ({pctOfRequired:F2}%)");
            Console.WriteLine($"Total hours: {emp.GetNrOfHoursForInterval(startDate, endDate)}");
        }

        Console.WriteLine("=============================================");
        var overallPct = totalRequiredOfficeDays == 0
            ? 100.0
            : (double)totalActualOfficeDays / totalRequiredOfficeDays * 100.0;

        Console.WriteLine($"Overall compliance (office days): {totalActualOfficeDays}/{totalRequiredOfficeDays} ({overallPct:F2}%)");
        Console.WriteLine("=============================================");
    }

    private static IEnumerable<DateOnly> EnumerateDays(DateOnly start, DateOnly end)
    {
        for (var d = start; d <= end; d = d.AddDays(1))
            yield return d;
    }
}
