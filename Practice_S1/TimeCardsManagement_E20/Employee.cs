namespace TimeCardsManagement_E20;

public class Employee
{
    private readonly List<AttendanceRecord> _attendanceRecords = [];
    public string Name { get; }
    public string EmployeeID { get; }

    public Employee(string name, string employeeID)
    {
        Name = name;
        EmployeeID = employeeID;
    }

    public void AddAttendanceRecords(List<AttendanceRecord> attendanceRecords)
        => _attendanceRecords.AddRange(attendanceRecords);

    public void EditRecord(DateOnly date, string? projectId = null, int? nrHours = null, string? task = null, string? location = null)
    {
        var record = _attendanceRecords.FirstOrDefault(r => r.Date == date);
        if (record == null)
        {
            Console.WriteLine($"No record found for {date}.");
            return;
        }
        record.ProjectId = projectId ?? record.ProjectId;
        record.NrHours = nrHours ?? record.NrHours;
        record.Task = task ?? record.Task;
        record.Location = location ?? record.Location;
    }

    public int GetNrOfHoursForInterval(DateOnly startDate, DateOnly stopDate)
    {
        var daysInInterval = _attendanceRecords.Where(r => r.Date >=  startDate && r.Date <= stopDate).ToList();
        return daysInInterval.Sum(d => d.NrHours);
    }

    public List<AttendanceRecord> GetRecords(DateOnly startDate, DateOnly stopDate) 
        => _attendanceRecords.Where(r => r.Date >= startDate && r.Date < stopDate).ToList();
}
