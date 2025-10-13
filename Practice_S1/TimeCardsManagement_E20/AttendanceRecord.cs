using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCardsManagement_E20;

public class AttendanceRecord
{
    public string ProjectId { get; set; }
    public string Task {  get; set; }
    public DateOnly Date {  get; set; }
    public int NrHours { get; set; }
    public string Location { get; set; }
}
