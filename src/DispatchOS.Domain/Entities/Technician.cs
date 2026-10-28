using System;
using System.Collections.Generic;

using DispatchOS.Domain.ValueObjects;


namespace DispatchOS.Domain.Entities;

public class Technician
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public WorkingHours Shift { get; private set; }
    public Location CurrentLocation { get; private set; }

    private readonly List<Skill> _skills = new();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();

    public Technician(string name, WorkingHours shift, Location currentLocation)
    {
        Id = Guid.NewGuid();
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
        Shift = shift;
        CurrentLocation = currentLocation;
    }

    private Technician()
    {
        Name = null!;
        Shift = null!;
    }

    public void ChangeShift(WorkingHours newShift)
    {
        Shift = newShift;
    }

    public void MoveTo(Location location)
    {
        CurrentLocation = location;
    }

    public void AddSkill(Skill skill)
    {
        if (!_skills.Contains(skill))
        {
            _skills.Add(skill);
        }
    }

    public void RemoveSkill(Skill skill)
    {
        _skills.Remove(skill);
    }
    
    public bool HasRequiredSkills(IEnumerable<Skill> requiredSkills)
    {
        return requiredSkills.All(req => _skills.Contains(req));
    }
}