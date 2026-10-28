# [Dashboard] Blazor UI for tenants, dispatcher and technicians

Implement pages in `src/DispatchOS.Dashboard`. Pages, layout and `IDispatchApiClient` already exist.
By default `"UseFakeApi": true` → `FakeDispatchApiClient` (in-memory data), so the UI can be built before the API is done.

## Tenant
- [ ] `/report` – form (description, location X/Y, time window, priority, required skills, estimated duration) with validation → `ReportJobAsync` → redirect to `/jobs/{id}`.
- [ ] `/jobs/{id}` – status of the submission, estimated time (`PlannedStart` formatted as HH:mm), "not found" state.

## Dispatcher – `/dispatcher`
- [ ] Table of all jobs with status filter, priority and window.
- [ ] Manual assignment: choose technician (only those with required skills) → `AssignJobAsync`; button to remove manual assignment.
- [ ] Generate plan: date + strategy picker → `GeneratePlanAsync`.
- [ ] Show current plan: per-technician timeline/list (start–end, job), list of unassigned jobs with reasons, total cost, **warning when `IsStale`** + "Regenerate" button.
- [ ] "Accept plan" → `AcceptCurrentPlanAsync`.

## Technician – `/technician/{id}`
- [ ] Technician picker (no auth in MVP).
- [ ] List of my jobs ordered by planned start; buttons Start / Done / Cancel → `UpdateJobStatusAsync`.

## Integration
- [ ] Implement `HttpDispatchApiClient` against `DispatchOS.Api` using `DispatchOS.Contracts.Routes` (set `"UseFakeApi": false`).
- [ ] Handle API errors (409 / 404 / 501) with a visible message instead of crashing the circuit.

## Out of scope
- Backend logic (#1), algorithm (#2), auth, QR reporting.

## Definition of done
- All three views usable with the fake client; works end-to-end with the real API once #1 is merged.
