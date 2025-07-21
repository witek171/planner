using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedule.Application.Interfaces.Services;
using Schedule.Contracts.Dtos;
using Schedule.Domain.Models;

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

		return health.Status switch
		{
			"Healthy" => Ok(dto),
			"Degraded" => StatusCode(207, dto),
			"Unhealthy" => StatusCode(503, dto)
		};
	}

	[HttpGet("database")]
	public async Task<ActionResult<DatabaseHealthStatusDto>> GetDatabaseStatusAsync()
	{
		DatabaseHealthStatus health = await _healthCheckService.GetDatabaseStatusAsync();

		DatabaseHealthStatusDto dto = _mapper.Map<DatabaseHealthStatusDto>(health);

		return health.Status switch
		{
			"Healthy" => Ok(dto),
			"Degraded" => StatusCode(207, dto),
			"Unhealthy" => StatusCode(503, dto)
		};
	}
}