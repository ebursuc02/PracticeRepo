using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator_E13;

public static class SalaryCalculator
{
    private const double TAX = 0.10;
    private const double SOCIAL = 0.25;
    private const double HEALTH = 0.10;
    public static double CalcGrossMonthlyPay(Employee employee) 
        => employee.GetAdjustedPay() * employee.HoursPerWeek * 4;
    public static double CalcNetMonthlyPay(Employee employee)
    {
        var gross = CalcGrossMonthlyPay(employee);
        var deductions = gross * (TAX + SOCIAL + HEALTH);
        return gross - deductions;
    }

    public static double CalcTotalEarned(Employee employee)
    {
        int months = (int)(DateTime.Now - employee.HireDate).TotalDays / 30;
        return CalcNetMonthlyPay(employee) * months;
    }

    public static void DisplayInfo(Employee employee)
    {
        var gross = CalcGrossMonthlyPay(employee);
        var net = CalcNetMonthlyPay(employee);

        Console.WriteLine($"** {employee.Name} ({employee.EmployeeId}) **");
        Console.WriteLine($"Gross worth: {gross:F2}");
        Console.WriteLine($"Net worth: {net:F2}");
        Console.WriteLine($"Deductions: {(gross - net):F2}");
        Console.WriteLine($"Total earned in {employee.GetYearsAtCompany()} years: {CalcTotalEarned(employee):F2}");
    }

}
