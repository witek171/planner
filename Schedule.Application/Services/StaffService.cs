using Schedule.Contracts.Dtos;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Repositories;

namespace Schedule.Application.Services;

public class StaffService : IStaffService
{
    private readonly IStaffProfileRepository _repository;

    public StaffService(IStaffProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StaffProfileDto>> GetAllAsync()
    {
        var staff = await _repository.GetAllAsync();
        return staff.Select(s => new StaffProfileDto
        {
            Id = s.Id,
            ReceptionId = s.ReceptionId,
            UserId = s.UserId
        });
    }

    public async Task<StaffProfileDto?> GetByIdAsync(Guid id)
    {
        var s = await _repository.GetByIdAsync(id);
        if (s == null) return null;

        return new StaffProfileDto
        {
            Id = s.Id,
            ReceptionId = s.ReceptionId,
            UserId = s.UserId
        };
    }

    public async Task CreateAsync(StaffProfileDto dto)
    {
        var staff = new StaffProfile
        {
            Id = Guid.NewGuid(),
            ReceptionId = dto.ReceptionId,
            UserId = dto.UserId
        };

        await _repository.AddAsync(staff);
    }

    public async Task UpdateAsync(StaffProfileDto dto)
    {
        var staff = new StaffProfile
        {
            Id = dto.Id,
            ReceptionId = dto.ReceptionId,
            UserId = dto.UserId
        };

        await _repository.UpdateAsync(staff);
    }

    public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
}