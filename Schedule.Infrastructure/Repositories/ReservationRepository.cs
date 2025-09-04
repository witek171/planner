using Schedule.Application.Interfaces.Repositories;

namespace Schedule.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
	private readonly string _connectionString;

	public ReservationRepository(string connectionString)
	{
		_connectionString = connectionString;
	}
}