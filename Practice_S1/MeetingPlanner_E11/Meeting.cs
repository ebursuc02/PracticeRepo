namespace MeetingPlanner_E11;

public class Meeting
{
    public DateTime Time { get; set; }
    public Participant P1 { get; set; }
    public Participant P2 { get; set; }

    public Meeting(DateTime time, Participant p1, Participant p2)
    {
        Time = time;
        P1 = p1;
        P2 = p2;
        P1.Invite(this);
        P2.Invite(this);
    }

    public bool BothConfirmed() => P1.HasConfirmed(this) && P2.HasConfirmed(this);
}