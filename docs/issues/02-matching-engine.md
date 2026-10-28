# [Engine] Planning algorithm in DispatchOS.MatchingEngine

Implement `Planner.CreatePlan` (`src/DispatchOS.MatchingEngine/Planning/Planner.cs`). The HTTP endpoint `POST /plan` and contracts (`PlanRequest` / `PlanResponse`) already exist; currently the planner returns everything as unassigned.

## Hard constraints (must always hold)
- [ ] Technician has **all** required skills of the job.
- [ ] Assignment `[Start, End)` lies in the technician's shift **and** in the job's window; `End - Start == DurationMinutes`.
- [ ] No overlaps for one technician, including travel time between consecutive jobs (start from `StartLocation`, distance = Euclidean, 1 unit = 1 min).
- [ ] Each job at most once in `Assignments`; every job appears exactly once in `Assignments` ∪ `Unassigned`.
- [ ] `FixedAssignments` (manual dispatcher decisions) are returned unchanged and block the technician's time.
- [ ] Unassigned jobs get a meaningful `Reason` (e.g. "No technician with skills: hvac", "No free slot in window").

## Soft constraints / cost
- [ ] Cost function combining: travel distance, delay vs. window start, overtime, penalty for unassigned jobs weighted by `Priority`.
- [ ] Weights depend on strategy: `MaximizeJobsDone`, `MinimizeTravel`, `MinimizeOvertime`, `PriorityFirst` (`DispatchOS.Domain.Enums.PlanningStrategy`). Suggestion: one `IPlanningStrategy` (weights) per strategy.
- [ ] Return the total cost in `PlanResponse.Cost`.

## Algorithm
Suggested path: greedy construction (sort by priority/window end, insert at the cheapest feasible position) → optional local search improvement (move/swap jobs between technicians) with a time/iteration limit. Keep it deterministic for tests.

## Tests
- [ ] Remove `[Ignore]` from `tests/DispatchOS.MatchingEngine.Tests/PlannerTests.cs` and make them pass.
- [ ] Add tests for: travel time between jobs, fixed assignments blocking time, strategies producing different results, empty input.
- [ ] Optional: a bigger randomized test (e.g. 100 jobs, 15 technicians) checking hard constraints + runtime < 2 s.

## Out of scope
- Storing plans, state versioning (#1), UI (#3).

## Definition of done
- All hard constraints verified by tests, `dotnet test` green.
