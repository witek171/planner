using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IReservationRepository
{
	Task<Reservation?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(Reservation reservation);

	Task<bool> DeleteAsync(
		Guid id,
		Guid companyId);
}