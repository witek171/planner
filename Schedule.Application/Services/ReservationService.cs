using Schedule.Application.Interfaces.Repositories;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Models;

namespace Schedule.Application.Services;

public class ReservationService : IReservationService
{
	private readonly IReservationRepository _reservationRepository;

	public ReservationService(IReservationRepository reservationRepository)
	{
		_reservationRepository = reservationRepository;
	}

	public async Task<Reservation?> GetByIdAsync(
		Guid id,
		Guid companyId)
		=> await _reservationRepository.GetByIdAsync(id, companyId);

	public async Task<Guid> CreateAsync(Reservation reservation)
	{
		reservation.Normalize();
		return await _reservationRepository.CreateAsync(reservation);
	}

	public async Task DeleteAsync(
		Guid id,
		Guid companyId)
	{
		// Reservation? reservation = await reservationRepository.GetByIdAsync(id, companyId);
		// if (reservation == null)
		// 	throw new InvalidOperationException(
		// 		$"Reservation {id} is already marked as cancelled");
		//
		// reservation.SoftDelete();
		// await _reservationRepository.UpdateStatusAsync(reservation);
	}
}