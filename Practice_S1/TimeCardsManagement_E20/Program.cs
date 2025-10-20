using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Application.Services;
using TimeCardsManagement_E20.Domain.Entities;
using TimeCardsManagement_E20.Domain.Policies;
using TimeCardsManagement_E20.Infrastructure;

IHolidayPolicy holidays = new FixedHolidays(new[]
{
    new DateOnly(2025, 1, 1),
    new DateOnly(2025, 12, 25)
});
IAttendanceRequirementPolicy requirement = new FixedAttendanceRequirement(required: 2);

var catalog = new InMemoryProjectCatalog()
    .AddProject("P-100", "Migration",
        ("DES", "Design"),
        ("DEV", "Development"),
        ("TST", "Testing"))
    .AddProject("P-200", "Internal Tools",
        ("SUP", "Support"),
        ("RND", "R&D"));


var system = new AttendanceSystem(requirement, holidays, catalog);

var e = Employee.Create("Jane Doe", "E001").Value!;
system.AddEmployee(e);

var start = new DateOnly(2025, 10, 20);
var end = new DateOnly(2025, 10, 26);
var card = system.CreateTimeCard(e.EmployeeID, start, end).Value!;

// Add attendance in the created timecard
system.AddAttendanceToCard(card.Id, "P-100", "DES", new DateOnly(2025, 10, 20), 8, WorkLocation.Office);
system.AddAttendanceToCard(card.Id, "P-100", "DEV", new DateOnly(2025, 10, 21), 8, WorkLocation.Remote);
system.AddAttendanceToCard(card.Id, "P-200", "SUP", new DateOnly(2025, 10, 22), 8, WorkLocation.Office);
system.AddAttendanceToCard(card.Id, "P-100", "DES", new DateOnly(2025, 10, 23), 8, WorkLocation.Remote);

// Add leave that must be approved first
var leave = system.AddLeaveToCard(card.Id, new DateOnly(2025, 10, 24)).Value!;
leave.Approve(); // Manager approves

// Approve then submit timecard
system.ApproveTimeCard(card.Id);
system.SubmitTimeCard(card.Id);

// Add attendance records to the employee after approval
e.AddAttendanceRecords(card.Attendance);
e.AddAnnualLeaveRecords(card.Leave);


system.GetReportForInterval(start, end);
