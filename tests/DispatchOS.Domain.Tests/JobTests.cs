using DispatchOS.Domain.Entities;
using DispatchOS.Domain.Enums;
using DispatchOS.Domain.ValueObjects;

namespace DispatchOS.Domain.Tests;

public class JobTests
{
    private static Job NewJob() => new(
        "Leaking pipe", new Location(1, 2), new TimeWindow(480, 720), JobPriority.High, 60,
        new[] { new Skill("plumbing") });

    [Test]
    public void NewJob_IsReported()
    {
        Assert.That(NewJob().Status, Is.EqualTo(JobStatus.Reported));
    }

    [Test]
    public void Constructor_DurationLongerThanWindow_Throws()
    {
        Assert.Throws<ArgumentException>(() => new Job(
            "x", new Location(0, 0), new TimeWindow(480, 500), JobPriority.Low, 60, Array.Empty<Skill>()));
    }

    [Test]
    public void HappyPath_AssignStartComplete_EndsDone()
    {
        var job = NewJob();
        job.AssignTo(Guid.NewGuid(), manually: true);
        job.Start();
        job.Complete();

        Assert.That(job.Status, Is.EqualTo(JobStatus.Done));
    }

    [Test]
    public void Start_NotAssigned_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => NewJob().Start());
    }

    [Test]
    public void Unassign_ClearsTechnicianAndManualFlag()
    {
        var job = NewJob();
        job.AssignTo(Guid.NewGuid(), manually: true);
        job.Unassign();

        Assert.Multiple(() =>
        {
            Assert.That(job.Status, Is.EqualTo(JobStatus.Reported));
            Assert.That(job.AssignedTechnicianId, Is.Null);
            Assert.That(job.IsManuallyAssigned, Is.False);
        });
    }
}
