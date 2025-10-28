namespace SalaryCalculator_E13;

public class Employee
{
    public string Name { get; }
    public string EmployeeId { get; }
    public DateTime HireDate { get; }
    public double PayPerHour { get; private set; }
    public int HoursPerWeek { get; private set; }

    public Employee(string name, string employeeId, DateTime hireDate, double payPerHour, int hoursPerWeek)
    {
        Name = name;
        EmployeeId = employeeId;
        HireDate = hireDate;
        PayPerHour = payPerHour;
        HoursPerWeek = hoursPerWeek;
    }

    public int GetYearsAtCompany() => DateTime.Now.Year - HireDate.Year;

    public double GetAdjustedPay()
    {
        // +2% for each year (max 30%)
        int years = GetYearsAtCompany();
        double bonus = Math.Min(years * 0.02, 0.3);
        return PayPerHour * ( 1  + bonus );
    }
}
