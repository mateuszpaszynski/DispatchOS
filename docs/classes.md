Technician:
-id
-List<Skills>
-working hours
-current localization

Job
-id
-description
-window
-priority
-state (enum?)
-localization
-required skills to complete
-aprox time to complete

Plan
Jobs assigned
-List<{jobId,technicianId, starttime, endtime, isManuallySet}> 
-List<{jobId, reason, stateVersion, cost}>


