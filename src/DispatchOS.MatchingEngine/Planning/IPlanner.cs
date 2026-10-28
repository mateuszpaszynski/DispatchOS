using DispatchOS.Contracts.Planning;

namespace DispatchOS.MatchingEngine.Planning;

public interface IPlanner
{
    PlanResponse CreatePlan(PlanRequest request);
}
