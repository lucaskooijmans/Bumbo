using Data.Models;

namespace Data.Interfaces;

public interface IAvailabilityRepository
{

    // Get all availabilities
    List<Availability> GetAvailabilities();

    // Get availability by ID
    Availability GetAvailabilityById(int id);

    // Get availabilities of employee
    List<Availability> GetAvailabilitiesOfEmployee(int id);

    public void UpdateAvailabilities(List<Availability> availability);

    public void CreateAvailabilities(List<Availability> availability);

    public void DeleteAvailabilities(List<Availability> zeroHoursLeft);
}