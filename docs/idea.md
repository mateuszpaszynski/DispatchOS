DispatchOS:
    We have a firm with hundreds of rentals, and we hire technicians that are fixing 
    any incidents as they happen. Each technician has different specialization and skills, so we want to assign them
    to do repairs and minimize different factors such as overtime work, waiting time, or maximize jobs done (sometimes it won't be possible to finish all of them), goals
    are often mutually exclusive so we need to pick something

We plan for one day

Hard requirements: required skills, working hours, no jobs overlap for the same person, each job at most once, respect for manual managment

Soft requirements: distance travelled, delays, penalties for overtime


Dictionary:
User - a person renting a place from us 
Technican - different skills, working hours
Dispatcher - head of the repairs branch in our company
Job - localization, required skills, aproximate completion time, priority and window. 
Plan - assignment of jobs to technicians with time frames.

Users need to be able to: (MVP) 
- Report a malfunction
- See the status of their submission (eg estimated time)


Admin (Dispatcher):
- Needs to see all of the submissions
- Can manually assign technician to a job (which will be respected by algorithm while calculating suggested plan)
- Can generate suggested assignment with different strategies / or regenrate plan when the arrived solution is no longer valid (some new jobs came with high priority)



Technicians: 
- See the job that they have been assigned to
- Can update the state of the job


(After MVP):
- QR code based reporting
- AI text to tags or suggested preventing actions (eg cutting of water in case of broken valve)

Architecture:
Modular monolit for basic jobs reporting and viewing and one microservice for jobs matching engine

Blazor dashboard



