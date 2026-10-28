using DispatchOS.Contracts.Jobs;
using DispatchOS.Contracts.Planning;
using DispatchOS.MatchingEngine.Planning;

namespace DispatchOS.MatchingEngine.Tests;

// Acceptance tests for issue #2. Remove [Ignore] as the planner gets implemented.
public class PlannerTests
{
    private static readonly DateOnly Day = new(2026, 11, 2);
    private readonly IPlanner _planner = new Planner();

    private static PlanJob Job(int windowStart, int windowEnd, int duration, params string[] skills) =>
        new(Guid.NewGuid(), new LocationDto(0, 0), windowStart, windowEnd, 2, duration, skills);

    private static PlanTechnician Tech(int shiftStart, int shiftEnd, params string[] skills) =>
        new(Guid.NewGuid(), shiftStart, shiftEnd, new LocationDto(0, 0), skills);

    private static PlanRequest Request(IReadOnlyList<PlanJob> jobs, IReadOnlyList<PlanTechnician> techs,
        IReadOnlyList<PlannedAssignment>? fixedAssignments = null) =>
        new(Day, "MaximizeJobsDone", jobs, techs, fixedAssignments ?? Array.Empty<PlannedAssignment>());

    [Test]
    public void EveryJob_IsEitherAssignedOrUnassigned_ExactlyOnce()
    {
        var jobs = new[] { Job(480, 600, 60, "plumbing"), Job(480, 600, 60, "electric") };
        var result = _planner.CreatePlan(Request(jobs, new[] { Tech(480, 960, "plumbing") }));

        var ids = result.Assignments.Select(a => a.JobId).Concat(result.Unassigned.Select(u => u.JobId)).ToList();
        Assert.That(ids, Is.EquivalentTo(jobs.Select(j => j.Id)));
    }

    [Test]
    public void FixedAssignments_AreKept()
    {
        var job = Job(480, 600, 60, "plumbing");
        var tech = Tech(480, 960, "plumbing");
        var manual = new PlannedAssignment(job.Id, tech.Id, 500, 560, true);

        var result = _planner.CreatePlan(Request(new[] { job }, new[] { tech }, new[] { manual }));

        Assert.That(result.Assignments, Does.Contain(manual));
    }

    [Test, Ignore("TODO #2")]
    public void Job_IsAssigned_WhenSkilledTechnicianIsFree()
    {
        var job = Job(480, 600, 60, "plumbing");
        var tech = Tech(480, 960, "plumbing");

        var result = _planner.CreatePlan(Request(new[] { job }, new[] { tech }));

        Assert.That(result.Assignments.Single().TechnicianId, Is.EqualTo(tech.Id));
    }

    [Test, Ignore("TODO #2")]
    public void Job_IsNotAssigned_ToTechnicianWithoutRequiredSkills()
    {
        var result = _planner.CreatePlan(Request(new[] { Job(480, 600, 60, "electric") }, new[] { Tech(480, 960, "plumbing") }));

        Assert.That(result.Assignments, Is.Empty);
    }

    [Test, Ignore("TODO #2")]
    public void Assignments_RespectShiftAndJobWindow()
    {
        var job = Job(480, 600, 60, "plumbing");
        var tech = Tech(540, 960, "plumbing");

        var a = _planner.CreatePlan(Request(new[] { job }, new[] { tech })).Assignments.Single();

        Assert.Multiple(() =>
        {
            Assert.That(a.Start, Is.GreaterThanOrEqualTo(540));
            Assert.That(a.End, Is.LessThanOrEqualTo(600));
            Assert.That(a.End - a.Start, Is.EqualTo(60));
        });
    }

    [Test, Ignore("TODO #2")]
    public void Assignments_ForOneTechnician_DoNotOverlap()
    {
        var jobs = Enumerable.Range(0, 4).Select(_ => Job(480, 960, 90, "plumbing")).ToArray();
        var result = _planner.CreatePlan(Request(jobs, new[] { Tech(480, 960, "plumbing") }));

        var ordered = result.Assignments.OrderBy(a => a.Start).ToList();
        for (var i = 1; i < ordered.Count; i++)
            Assert.That(ordered[i].Start, Is.GreaterThanOrEqualTo(ordered[i - 1].End));
    }
}
