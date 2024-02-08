public class Record
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
    public TimeSpan Duration => CalculateDuration();
    public TimeSpan CalculateDuration()
    {
        TimeSpan Duration = EndTime - StartTime;
        return Duration;
    }
}
