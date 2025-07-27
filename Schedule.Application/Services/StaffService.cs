using AutoMapper;
using Schedule.Contracts.Dtos.Staff.Responses;
using Schedule.Contracts.Dtos.Staff.Requests;
using Schedule.Domain.Models;
using Schedule.Infrastructure.Repositories;
using Schedule.Application.Interfaces.Services;

namespace Schedule.Application.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _repository;
    private readonly IMapper _mapper;

    public StaffService(IStaffRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StaffResponse>> GetAllAsync()
    {
        var staffList = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<StaffResponse>>(staffList);
    }

    public async Task<StaffResponse?> GetByIdAsync(Guid id)
    {
        var staff = await _repository.GetByIdAsync(id);
        return staff is null ? null : _mapper.Map<StaffResponse>(staff);
    }

    public async Task<Guid> CreateAsync(CreateStaffRequest request)
    {
        var staff = _mapper.Map<Staff>(request);
        staff.Id = Guid.NewGuid();
        staff.CreatedAt = DateTime.UtcNow;

        return await _repository.CreateAsync(staff);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStaffRequest request)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        var updated = _mapper.Map(request, existing);
        return await _repository.UpdateAsync(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}