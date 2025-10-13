using TimeCardsManagement_E20;

AttendanceSystem system = new AttendanceSystem(2);

var employees = new List<Employee>
{
    new("Alice Johnson", "EMP001"),
    new("Bob Smith", "EMP002"),
    new("Charlie Davis", "EMP003")
};

var startDate = new DateOnly(2025, 10, 13);
var workDays = Enumerable.Range(0, 5).Select(offset => startDate.AddDays(offset));

// assign time card records
foreach (var employee in employees)
{
    system.AddEmployee(employee);
    List<AttendanceRecord> records = [];
    foreach (var day in workDays)
    {
        records.Add(new AttendanceRecord
        {
            ProjectId = "PRJ-" + (100 + employees.IndexOf(employee)),
            Task = "Development",
            Date = day,
            NrHours = 8,
            Location = "remote"
        });
    }
    employee.AddAttendanceRecords(records);
}

// edit records
employees[0].EditRecord(startDate, location: "office");
employees[0].EditRecord(startDate.AddDays(2), location: "office");

employees[2].EditRecord(startDate.AddDays(1), location: "office");
employees[2].EditRecord(startDate.AddDays(3), location: "office");

system.GetReportForInterval(startDate, startDate.AddDays(5));