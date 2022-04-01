namespace ConferencePlanner.Data.Models;

public class SessionSpeaker
{
    public int SessionId { get; set; }

    public virtual Session? Session { get; set; }

    public int SpeakerId { get; set; }

    public virtual Speaker? Speaker { get; set; }
}
