using DispatchOS.Contracts.Planning;

namespace DispatchOS.MatchingEngine.Planning;

// Hard constraints (must always hold):
//  - technician has all required skills
//  - assignment fits in the technician's shift and in the job's window
//  - no overlapping assignments for one technician (including travel time between jobs)
//  - each job assigned at most once
//  - FixedAssignments (manual dispatcher decisions) are kept as-is
// Soft constraints (minimized via cost, weighted by strategy): travel distance, delays, overtime, unassigned priority.
public class Planner : IPlanner
{
    public PlanResponse CreatePlan(PlanRequest request)
    {
        // TODO(#2): replace with a real algorithm. For now: keep manual assignments, leave everything else unassigned.
        var fixedJobIds = request.FixedAssignments.Select(a => a.JobId).ToHashSet();
        var unassigned = request.Jobs
            .Where(j => !fixedJobIds.Contains(j.Id))
            .Select(j => new PlannedUnassignedJob(j.Id, "Not implemented"))
            .ToList();

        return new PlanResponse(0, request.FixedAssignments, unassigned);
    }
}
