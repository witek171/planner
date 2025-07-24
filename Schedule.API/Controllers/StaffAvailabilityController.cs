using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces;
using Schedule.Contracts.Dtos;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffAvailabilityController : ControllerBase
{
	private readonly IStaffAvailabilityService _service;

	public StaffAvailabilityController(IStaffAvailabilityService service)
	{
		_service = service;
	}

	[HttpGet("staff/{staffId}")]
	public ActionResult<IEnumerable<StaffAvailabilityDto>> GetByStaff(Guid staffId)
	{
		var list = _service.GetByStaffId(staffId);
		return Ok(list);
	}

	[HttpPost]
	public IActionResult Create([FromBody] StaffAvailabilityDto dto)
	{
		_service.Create(dto);
		return Ok();
	}

	[HttpPut]
	public IActionResult Update([FromBody] StaffAvailabilityDto dto)
	{
		_service.Update(dto);
		return Ok();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(Guid id)
	{
		_service.Delete(id);
		return NoContent();
	}
}
