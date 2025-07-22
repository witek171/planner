using Microsoft.AspNetCore.Mvc;
using Schedule.Contracts.Dtos;
using Schedule.Infrastructure.Repositories;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/staff-specializations")]
public class StaffSpecializationController : ControllerBase
{
    private readonly IStaffSpecializationRepository _repo;

    public StaffSpecializationController(IStaffSpecializationRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public IActionResult Create([FromBody] StaffSpecializationDto dto)
    {
        _repo.Create(dto);
        return Ok();
    }

    [HttpGet("{staffId}")]
    public IActionResult GetByStaffId(Guid staffId)
    {
        var items = _repo.GetByStaffId(staffId);
        return Ok(items);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _repo.Delete(id);
        return NoContent();
    }
}
