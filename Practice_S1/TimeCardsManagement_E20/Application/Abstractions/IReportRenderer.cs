using TimeCardsManagement_E20.Application.DTOs;
using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Application.Abstractions;

internal interface IReportRenderer
{
    void Render(AttendanceReport report);
}
