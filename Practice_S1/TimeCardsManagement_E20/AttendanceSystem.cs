namespace TimeCardsManagement_E20;

public class AttendanceSystem
{
    private readonly List<Employee> _employees = [];
    private readonly int _requiredNrDays = 0;

    public AttendanceSystem(int requiredNrDays) => _requiredNrDays = requiredNrDays;

    public void AddEmployee(Employee employee) => _employees.Add(employee);

    public void GetReportForInterval(DateOnly startDate, DateOnly endDate)
    {
        var employeesWithLessOfficeDays = new List<Employee>();
        int overallOfficeDays = 0;

        // Inclusive day count
        int totalDaysInRange = endDate.DayNumber - startDate.DayNumber + 1;
        if (totalDaysInRange <= 0)
        {
            Console.WriteLine("Invalid date interval.");
            return;
        }

        foreach (var emp in _employees)
        {
            Console.WriteLine("*************************************");
            Console.WriteLine($"{emp.Name} ({emp.EmployeeID})");

            var empRecords = emp.GetRecords(startDate, endDate);

            var daysWorked = empRecords.Select(r => r.Date).Distinct().Count();

            var officeDays = empRecords
                .Where(r => string.Equals(r.Location, "office"))
                .Select(r => r.Date)
                .Distinct()
                .Count();

            foreach (var record in empRecords)
            {
                Console.WriteLine($"{record.ProjectId} | {record.Task} | {record.Location} | {record.Date} | {record.NrHours} hours");
            }

            Console.WriteLine($"Total nr. hours: {emp.GetNrOfHoursForInterval(startDate, endDate)}");

            if( daysWorked != 0 )
            {
                double empAttendancePct = (officeDays / (double)daysWorked) * 100.0;
                Console.WriteLine($"Attendance percentage: {empAttendancePct:F2}%");
            }
            Console.WriteLine("*************************************\n");

            if (officeDays < _requiredNrDays) employeesWithLessOfficeDays.Add(emp);
            overallOfficeDays += officeDays;
        }

        Console.WriteLine("Employees with less nr. of days at office:");
        foreach (var emp in employeesWithLessOfficeDays)
            Console.WriteLine(emp.Name);

        double overallPct = (overallOfficeDays / (double)(_employees.Count * totalDaysInRange)) * 100.0;
        Console.WriteLine($"Overall attendance: {overallPct:F2}%");
    }

}