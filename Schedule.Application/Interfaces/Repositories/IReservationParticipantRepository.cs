using Schedule.Domain.Models;

namespace Schedule.Application.Interfaces.Repositories;

public interface IReservationParticipantRepository
{
	Task<List<Guid>> GetIdsByReservationIdAsync(
		Guid reservationId,
		Guid companyId);

	Task<Guid> CreateAsync(ReservationParticipant reservationParticipant);

	Task<bool> DeleteAsync(
		Guid id,
		Guid companyId);
}