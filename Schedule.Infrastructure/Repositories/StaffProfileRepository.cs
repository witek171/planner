using Schedule.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Schedule.Infrastructure.Repositories;

public class StaffProfileRepository : IStaffProfileRepository
{
    private readonly ScheduleDbContext _context;

    public StaffProfileRepository(ScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StaffProfile>> GetAllAsync() =>
        await _context.StaffProfiles.ToListAsync();

    public async Task<StaffProfile?> GetByIdAsync(Guid id) =>
        await _context.StaffProfiles.FindAsync(id);

    public async Task AddAsync(StaffProfile staff)
    {
        _context.StaffProfiles.Add(staff);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StaffProfile staff)
    {
        _context.StaffProfiles.Update(staff);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.StaffProfiles.FindAsync(id);
        if (entity != null)
        {
            _context.StaffProfiles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
