using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Services;
using Schedule.Contracts.Dtos.StaffRelated.Staff.Requests;
using Schedule.Contracts.Dtos.StaffRelated.Staff.Responses;
using Schedule.Domain.Models.Staff;

namespace Schedule.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly StaffService _service;
    private readonly IMapper _mapper;

    public StaffController(StaffService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<StaffResponse>> GetAll()
    {
        var staffList = _service.GetAll();
        var response = _mapper.Map<IEnumerable<StaffResponse>>(staffList);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<StaffResponse> GetById(Guid id)
    {
        var staff = _service.GetById(id);
        if (staff is null) return NotFound();
        var response = _mapper.Map<StaffResponse>(staff);
        return Ok(response);
    }

    [HttpPost]
    public ActionResult<Guid> Create([FromBody] CreateStaffRequest request)
    {
        var staff = _mapper.Map<Staff>(request);
        var id = _service.Create(staff);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateStaffRequest request)
    {
        var staff = _mapper.Map<Staff>(request);
        staff.Id = id;
        _service.Update(staff);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _service.Delete(id);
        return NoContent();
    }
}
