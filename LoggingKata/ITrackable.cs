namespace LoggingKata
{
    // Interface for Tracking implementation
    public interface ITrackable
    {
        string Name { get; set; }
        Point Location { get; set; }
    }
}