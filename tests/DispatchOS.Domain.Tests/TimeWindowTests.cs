using DispatchOS.Domain.ValueObjects;

namespace DispatchOS.Domain.Tests;

public class TimeWindowTests
{
    [Test]
    public void Overlaps_TouchingWindows_ReturnsFalse()
    {
        Assert.That(new TimeWindow(480, 540).Overlaps(new TimeWindow(540, 600)), Is.False);
    }

    [Test]
    public void Overlaps_IntersectingWindows_ReturnsTrue()
    {
        Assert.That(new TimeWindow(480, 560).Overlaps(new TimeWindow(540, 600)), Is.True);
    }

    [Test]
    public void Constructor_EndBeforeStart_Throws()
    {
        Assert.Throws<ArgumentException>(() => new TimeWindow(600, 480));
    }
}
