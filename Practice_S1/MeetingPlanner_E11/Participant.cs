namespace MeetingPlanner_E11;

public class Participant
{
    private List<Meeting> _meetings = [];
    private List<Meeting> _confirmedMeetings = [];
    public string Name { get; }

    public Participant(string name) => Name = name;
    
    public void ConfirmMeeting(Meeting meeting) => _confirmedMeetings.Add(meeting);

    public bool HasConfirmed(Meeting meeting) => _confirmedMeetings.Any(m =>  m.Equals(meeting));

    public void Invite(Meeting meeting) => _meetings.Add(meeting);
}
