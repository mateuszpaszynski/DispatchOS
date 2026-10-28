# DispatchOS

Planning repairs for a rental company: tenants report malfunctions, a dispatcher assigns technicians
(manually or via a suggested plan), technicians update job status. See [docs/idea.md](docs/idea.md).

## Structure

| Project | What | Port |
|---|---|---|
| `src/DispatchOS.Domain` | Entities (`Job`, `Technician`, `Plan`), value objects, enums. No dependencies. | – |
| `src/DispatchOS.Contracts` | DTOs + `Routes` shared by all services. No dependencies. | – |
| `src/DispatchOS.Api` | Modular monolith: `Modules/Jobs`, `Modules/Technicians`, `Modules/Planning`. | 5100 |
| `src/DispatchOS.MatchingEngine` | Microservice computing suggested plans (`POST /plan`). | 5200 |
| `src/DispatchOS.Dashboard` | Blazor Server UI for tenants, dispatcher and technicians. | 5300 |

```
Dashboard ──HTTP──▶ Api ──HTTP──▶ MatchingEngine
     └──── all use DispatchOS.Contracts ────┘
```

## Conventions

- Planning horizon is **one day**. All times (`WorkingHours`, `TimeWindow`, plan start/end) are **minutes from midnight** (08:00 = 480).
- Locations are points on a grid; travel time = Euclidean distance (1 unit = 1 minute) until we decide otherwise.
- `Status` / `Priority` / `Strategy` in DTOs are the enum names from `DispatchOS.Domain.Enums`.
- Changing a contract in `DispatchOS.Contracts` affects everyone — do it in a separate small PR and ping the others.

## Run

```bash
dotnet test
dotnet run --project src/DispatchOS.MatchingEngine
dotnet run --project src/DispatchOS.Api
dotnet run --project src/DispatchOS.Dashboard   # "UseFakeApi": true by default
```
