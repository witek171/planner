using AutoMapper;
using Schedule.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Domain.Dtos;

namespace PlannerNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
	private readonly IHealthCheckService _healthCheckService;
	private readonly IMapper _mapper;

	public HealthCheckController(
		IHealthCheckService healthCheckService,
		IMapper mapper
	)
	{
		_healthCheckService = healthCheckService;
		_mapper = mapper;
	}

	[HttpGet("application")]
	public ActionResult<ApplicationHealthStatusDto> GetApplicationStatus()
	{
		ApplicationHealthStatus health = _healthCheckService.GetApplicationStatus();

		ApplicationHealthStatusDto dto = _mapper.Map<ApplicationHealthStatusDto>(health);

		return health.Status == "Healthy" ? Ok(dto) : StatusCode(503, dto);
	}

	[HttpGet("database")]
	public async Task<ActionResult<DatabaseHealthStatusDto>> GetDatabaseStatusAsync()
	{
		DatabaseHealthStatus health = await _healthCheckService.GetDatabaseStatusAsync();

		DatabaseHealthStatusDto dto = _mapper.Map<DatabaseHealthStatusDto>(health);

		return health.Status == "Healthy" ? Ok(dto) : StatusCode(503, dto);
	}
}