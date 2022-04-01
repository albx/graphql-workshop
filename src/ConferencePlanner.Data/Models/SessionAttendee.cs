namespace ConferencePlanner.Data.Models;

public class SessionAttendee
{
    public int SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public int AttendeeId { get; set; }

    public virtual Attendee? Attendee { get; set; }
}
