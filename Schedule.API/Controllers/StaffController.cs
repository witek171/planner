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
	public IActionResult GetAll() => Ok(_staffService.GetAll());

	[HttpGet("{id}")]
	public IActionResult GetById(Guid id)
	{
		var staff = _staffService.GetById(id);
		return staff == null ? NotFound() : Ok(staff);
	}

	[HttpPost]
	public IActionResult Create([FromBody] StaffDto staff)
	{
		if (staff.Id == Guid.Empty)
		{
			staff.Id = Guid.NewGuid();
			staff.CreatedAt = DateTime.UtcNow;
		}
		_staffService.Create(staff);
		return CreatedAtAction(nameof(GetById), new { id = staff.Id }, staff);
	}

	[HttpPut("{id}")]
	public IActionResult Update(Guid id, [FromBody] StaffDto staff)
	{
		if (id != staff.Id)
			return BadRequest("ID mismatch");

		if (_staffService.GetById(id) == null)
			return NotFound();

		_staffService.Update(staff);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(Guid id)
	{
		if (_staffService.GetById(id) == null)
			return NotFound();

		_staffService.Delete(id);
		return NoContent();
	}
}
