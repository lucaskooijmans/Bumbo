using System;
using Data.Enums;
using Data.Interfaces;
using Data.Models;

namespace Data.Repository;

public class AvailabilityRepository : IAvailabilityRepository
{
	private readonly BumboContext _context;

	public AvailabilityRepository(BumboContext context)
	{
		_context = context;
	}
    
    public List<Availability> GetAvailabilities()
    {
        return _context.Availabilities.ToList();
    }

    public List<Availability> GetAvailabilitiesOfEmployee(int employeeId)
    {
        return _context.Availabilities
            .Where(av => av.EmployeeId == employeeId)
            .ToList();
    }

    public Availability GetAvailabilityById(int id)
    {
        return _context.Availabilities.Find(id);
    }

    public void UpdateAvailabilities(List<Availability> availability)
    {
        _context.Availabilities.UpdateRange(availability);
        _context.SaveChanges();
    }
    
    public void CreateAvailabilities(List<Availability> availability)
    {
        _context.Availabilities.AddRange(availability);
        _context.SaveChanges();
    }
    public void DeleteAvailabilities(List<Availability> zeroHoursLeft)
    {
        _context.Availabilities.RemoveRange(zeroHoursLeft);
        _context.SaveChanges();
    }
}