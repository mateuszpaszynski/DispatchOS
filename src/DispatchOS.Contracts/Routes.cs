namespace DispatchOS.Contracts;

// Single source of truth for HTTP routes, shared by servers and clients.
public static class Routes
{
    public static class Api
    {
        public const string Jobs = "/api/jobs";
        public const string Job = "/api/jobs/{id:guid}";
        public const string JobStatus = "/api/jobs/{id:guid}/status";
        public const string JobAssignment = "/api/jobs/{id:guid}/assignment";

        public const string Technicians = "/api/technicians";
        public const string Technician = "/api/technicians/{id:guid}";
        public const string TechnicianJobs = "/api/technicians/{id:guid}/jobs";

        public const string Plans = "/api/plans";
        public const string CurrentPlan = "/api/plans/current";
        public const string AcceptCurrentPlan = "/api/plans/current/accept";
    }

    public static class MatchingEngine
    {
        public const string Plan = "/plan";
    }
}
