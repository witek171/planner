using Schedule.Contracts.Dtos;

namespace Schedule.Infrastructure.Repositories;

public interface IStaffRepository
{
	IEnumerable<StaffDto> GetAll();
	StaffDto? GetById(Guid id);
	void Create(StaffDto staff);
	void Update(StaffDto staff);
	void Delete(Guid id);
}