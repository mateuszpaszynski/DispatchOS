using DispatchOS.Domain.ValueObjects;

namespace DispatchOS.Domain.Tests;

public class LocationTests
{
    [Test]
    public void DistanceTo_Points34And00_ReturnsFive()
    {
        var a = new Location(0, 0);
        var b = new Location(3, 4);

        Assert.That(a.DistanceTo(b), Is.EqualTo(5).Within(0.0001));
    }
}