using Schedule.Application.Interfaces.Repositories;
using Schedule.Domain.Models;

namespace Schedule.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
	private readonly string _connectionString;

	public ReservationRepository(string connectionString)
	{
		_connectionString = connectionString;
	}

	public Task<Reservation?> GetByIdAsync(Guid id, Guid companyId)
	{
		throw new NotImplementedException();
	}

	public Task<Guid> CreateAsync(Reservation reservation)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteAsync(Guid id, Guid companyId)
	{
		throw new NotImplementedException();
	}
}