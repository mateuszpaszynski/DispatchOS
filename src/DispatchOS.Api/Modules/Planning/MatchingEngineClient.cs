using DispatchOS.Contracts;
using DispatchOS.Contracts.Planning;

namespace DispatchOS.Api.Modules.Planning;

public class MatchingEngineClient(HttpClient http)
{
    public async Task<PlanResponse> CreatePlanAsync(PlanRequest request, CancellationToken ct = default)
    {
        var response = await http.PostAsJsonAsync(Routes.MatchingEngine.Plan, request, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<PlanResponse>(ct))!;
    }
}
