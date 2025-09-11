using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Services;

public interface IReservationService
{
	Task<List<Reservation>> GetAllAsync(Guid companyId);

	Task<Reservation?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(Reservation reservation);
	Task UpdateAsync(Reservation reservation);


	Task DeleteAsync(
		Guid id,
		Guid companyId);
}