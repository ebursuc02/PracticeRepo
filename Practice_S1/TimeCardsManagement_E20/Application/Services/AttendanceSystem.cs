using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Application.DTOs;
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
    public Result<AttendanceReport> GetReportForInterval(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
            return Result<AttendanceReport>.Fail("Invalid date interval");

        var report = new AttendanceReport
        {
            StartDate = startDate,
            EndDate = endDate
        };

        int totalRequired = 0;
        int totalActual = 0;

        foreach (var emp in _employees)
        {
            var attendance = emp.GetAttendanceRecords(startDate, endDate);
            var leaves = emp.GetLeaveRecords(startDate, endDate);
            var approvedLeaveDays = leaves.Count(l => l.IsApproved);

            int workedDays = attendance.Select(r => r.Date).Distinct().Count();
            int officeDays = attendance.Where(r => r.Location == WorkLocation.Office)
                                       .Select(r => r.Date).Distinct().Count();

            int businessDays = EnumerateDays(startDate, endDate)
                .Count(d => !_holidayPolicy.IsHoliday(d, emp));

            int businessMinusLeave = Math.Max(0, businessDays - approvedLeaveDays);
            int requiredOfficeDays = _requirementPolicy.ComputeRequiredOfficeDays(businessMinusLeave, emp);

            totalRequired += requiredOfficeDays;
            totalActual += officeDays;

            report.Employees.Add(new EmployeeAttendanceSummary
            {
                Name = emp.Name,
                EmployeeID = emp.EmployeeID,
                Attendance = attendance.OrderBy(r => r.Date).ToList(),
                WorkedDays = workedDays,
                OfficeDays = officeDays,
                ApprovedLeaveDays = approvedLeaveDays,
                BusinessDays = businessDays,
                RequiredOfficeDays = requiredOfficeDays,
                TotalHours = emp.GetNrOfHoursForInterval(startDate, endDate)
            });
        }

        report.TotalRequiredOfficeDays = totalRequired;
        report.TotalActualOfficeDays = totalActual;

        return Result<AttendanceReport>.Ok(report);
    }


    private static IEnumerable<DateOnly> EnumerateDays(DateOnly start, DateOnly end)
    {
        for (var d = start; d <= end; d = d.AddDays(1))
            yield return d;
    }
}