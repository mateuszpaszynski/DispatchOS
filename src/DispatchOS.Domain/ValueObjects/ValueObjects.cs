namespace DispatchOS.Domain.ValueObjects;

public readonly record struct Location(int X, int Y)
{
    public double DistanceTo(Location location)
    {
        return Math.Sqrt(Math.Pow(location.X - this.X, 2) + Math.Pow(location.Y - this.Y,2));
    }
}
public record Skill(string Name);

// All times are minutes from midnight of the planned day (e.g. 8:00 = 480).
public record WorkingHours(int Start, int End)
{
    public bool CanAccommodate(int jobStart, int jobEnd)
    {
        return jobStart >= Start && jobEnd <= End;
    }
}

public record TimeWindow
{
    public int Start { get; }
    public int End { get; }

    public TimeWindow(int start, int end)
    {
        if (start < 0 || end <= start)
            throw new ArgumentException("Window must have 0 <= start < end.");
        Start = start;
        End = end;
    }

    public int Duration => End - Start;

    public bool Contains(int start, int end)
    {
        return start >= Start && end <= End;
    }

    public bool Overlaps(TimeWindow other)
    {
        return Start < other.End && other.Start < End;
    }
}
