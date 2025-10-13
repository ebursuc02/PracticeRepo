namespace TimeCardsManagement_E20;

public class AttendanceSystem
{
    private readonly List<Employee> _employees = [];
    private readonly int _requiredNrDays = 0;

    public AttendanceSystem(int requiredNrDays) => _requiredNrDays = requiredNrDays;

    public void AddEmployee(Employee employee) => _employees.Add(employee);

    public void GetReportForInterval(DateOnly startDate, DateOnly endDate)
    {
        List<Employee> employeesWithLessOfficeDays = [];
        foreach (var emp in _employees)
        {
            var officeDays = 0;
            Console.WriteLine("*************************************");
            Console.WriteLine($"{emp.Name} ({emp.EmployeeID})");
            foreach (var record in emp.GetRecords(startDate, endDate))
            {
                if (record.Location == "office") officeDays++;
                Console.WriteLine($"{record.ProjectId} | {record.Task} | {record.Location} | {record.Date} | {record.NrHours} hours");
            }
            Console.WriteLine($"Total nr. hours: {emp.GetNrOfHoursForInterval(startDate, endDate)}");
            Console.WriteLine("*************************************\n");
            if (officeDays < _requiredNrDays) employeesWithLessOfficeDays.Add(emp); 
        }

        Console.WriteLine("Employees with less nr. of days at office:");
        foreach(var emp in employeesWithLessOfficeDays)
            Console.WriteLine(emp.Name);
    }
}