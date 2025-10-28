using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Application.DTOs;

namespace TimeCardsManagement_E20.Infrastructure.Rendering
{
    public sealed class ConsoleReportRenderer : IReportRenderer
    {
        public void Render(AttendanceReport report)
        {
            Console.WriteLine($"ATTENDANCE REPORT [{report.StartDate:yyyy-MM-dd} .. {report.EndDate:yyyy-MM-dd}]");

            foreach (var emp in report.Employees)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"{emp.Name} ({emp.EmployeeID})");

                foreach (var r in emp.Attendance)
                    Console.WriteLine($"{r.Date:yyyy-MM-dd} | {r.ProjectId} | {r.Task} | {r.Location} | {r.Hours}h");

                Console.WriteLine($"Worked days: {emp.WorkedDays}");
                Console.WriteLine($"Office days: {emp.OfficeDays}");
                Console.WriteLine($"Approved leave days: {emp.ApprovedLeaveDays}");
                Console.WriteLine($"Business days (excl. weekends/holidays): {emp.BusinessDays}");
                Console.WriteLine($"Required office days (policy): {emp.RequiredOfficeDays}");
                Console.WriteLine($"Attendance % of worked days: {emp.AttendancePctOfWorked:F2}%");
                Console.WriteLine($"Compliance vs required: {emp.OfficeDays}/{emp.RequiredOfficeDays} ({emp.CompliancePct:F2}%)");
                Console.WriteLine($"Total hours: {emp.TotalHours}");
            }

            Console.WriteLine("=============================================");
            Console.WriteLine($"Overall compliance (office days): {report.TotalActualOfficeDays}/{report.TotalRequiredOfficeDays} ({report.OverallCompliancePct:F2}%)");
            Console.WriteLine("=============================================");
        }
    }

}
