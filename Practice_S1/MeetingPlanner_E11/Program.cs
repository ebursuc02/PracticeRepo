using MeetingPlanner_E11;
using System.Linq.Expressions;

Participant p1 = new Participant("John Doe");
Participant p2 = new Participant("Alice Doe");

Meeting meeting = new Meeting(new DateTime(2025, 10, 30, 15, 30, 0), p1, p2);

p1.ConfirmMeeting(meeting);
p2.ConfirmMeeting(meeting);

if (meeting.BothConfirmed())
    Console.WriteLine($"Meeting on {meeting.Time:dddd}, {meeting.Time:dd} of {meeting.Time:MMMM} {meeting.Time:yyyy} has been confirmed by: {p1.Name} and {p2.Name}.");