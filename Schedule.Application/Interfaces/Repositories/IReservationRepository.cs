using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IReservationRepository
{
	Task<List<Reservation>> GetAllAsync(Guid companyId);

	Task<Reservation?> GetByIdAsync(
		Guid id,
		Guid companyId);

	Task<Guid> CreateAsync(Reservation reservation);
	Task<bool> UpdateAsync(Reservation reservation);

	Task<bool> UpdateSoftDeleteAsync(Reservation reservation);
	Task<bool> UpdatePaymentDetailsAsync(Reservation reservation);
}