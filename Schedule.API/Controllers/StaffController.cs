using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Services;
using Schedule.Contracts.Dtos;

namespace Schedule.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<StaffProfileDto>> GetAll()
    {
        var profiles = _staffService.GetAll();
        return Ok(profiles);
    }

    [HttpGet("{id}")]
    public ActionResult<StaffProfileDto> GetById(Guid id)
    {
        var profile = _staffService.GetById(id);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPost]
    public ActionResult Create([FromBody] StaffProfileDto profile)
    {
        // Jeśli Id jest guidem i nie ustawiony, można ustawić tutaj
        if (profile.Id == Guid.Empty)
            profile.Id = Guid.NewGuid();

        _staffService.Create(profile);
        return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
    }

    [HttpPut("{id}")]
    public ActionResult Update(Guid id, [FromBody] StaffProfileDto profile)
    {
        if (id != profile.Id)
            return BadRequest("ID mismatch");

        var existing = _staffService.GetById(id);
        if (existing == null)
            return NotFound();

        _staffService.Update(profile);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var existing = _staffService.GetById(id);
        if (existing == null)
            return NotFound();

        _staffService.Delete(id);
        return NoContent();
    }
}
