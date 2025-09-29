using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Validators;

public interface IAvailabilityCalculator
{
	Task<List<StaffMemberAvailability>> CalculateAvailableTimeSlots(
		List<StaffMemberAvailability> staffMemberAvailabilities,
		List<EventSchedule> staffMemberEvents,
		Guid companyId);
}