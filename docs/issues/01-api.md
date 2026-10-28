# [API] Jobs, Technicians and Planning modules in DispatchOS.Api

Implement the modular monolith backend. Skeleton with all routes already exists (`src/DispatchOS.Api/Modules/*`), every endpoint currently returns `501`.

## Scope

### Persistence
- [ ] Implement `IJobRepository` and `ITechnicianRepository` (EF Core + SQLite recommended; in-memory is OK as a first step).
- [ ] Seed a few technicians and jobs on startup (Development only).

### Jobs module (`Modules/Jobs/JobsModule.cs`)
- [ ] `POST /api/jobs` – report a malfunction, validate input (window, duration fits window, priority name), return `201` + `JobDto`.
- [ ] `GET /api/jobs?status=` – list all jobs for dispatcher.
- [ ] `GET /api/jobs/{id}` – job status for tenant, incl. `PlannedStart/PlannedEnd` (= estimated time).
- [ ] `PUT /api/jobs/{id}/status` – technician moves job `Assigned → InProgress → Done` (or `Cancelled`); invalid transition → `409`.
- [ ] `PUT/DELETE /api/jobs/{id}/assignment` – manual assignment by dispatcher (`Job.AssignTo(..., manually: true)`), must check technician skills.
- [ ] Domain → DTO mapping.

### Technicians module
- [ ] `GET /api/technicians`, `GET /api/technicians/{id}`, `POST /api/technicians`.
- [ ] `GET /api/technicians/{id}/jobs` – jobs assigned to a technician, ordered by planned start.

### Planning module
- [ ] State version: counter incremented on every change of jobs/technicians (new job, status change, manual assignment…).
- [ ] `POST /api/plans` – build `PlanRequest` (open jobs, technicians, manual assignments as `FixedAssignments`), call `MatchingEngineClient`, store result as `Plan` with current state version.
- [ ] `GET /api/plans/current` – last plan as `PlanDto` with `IsStale`.
- [ ] `POST /api/plans/current/accept` – apply plan assignments to jobs (`AssignTo(..., manually: false)`), save planned times.

### Tests
- [ ] Integration tests (`tests/DispatchOS.Api.Tests`, `WebApplicationFactory`) for the main flows; the engine can be faked by swapping `MatchingEngineClient`'s `HttpMessageHandler`.

## Out of scope
- Planning algorithm (#2), UI (#3), auth.

## Definition of done
- All endpoints from `DispatchOS.Contracts.Routes.Api` work, `dotnet test` green.
- Dashboard with `"UseFakeApi": false` works against it end to end.
