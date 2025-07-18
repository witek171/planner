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

	public HealthCheckController(IHealthCheckService healthCheckService)
	{
		_healthCheckService = healthCheckService;
	}

	[HttpGet("application")]
	public ActionResult<ApplicationHealthStatusDto> GetApplicationStatus()
	{
		ApplicationHealthStatus health = _healthCheckService.GetApplicationStatus();
        
		ApplicationHealthStatusDto dto = new ApplicationHealthStatusDto(health);

		return health.Status == "Healthy" ? Ok(dto) : StatusCode(503, dto);
	}

	[HttpGet("database")]
	public async Task<ActionResult<DatabaseHealthStatusDto>> GetDatabaseStatusAsync()
	{
		DatabaseHealthStatus health = await _healthCheckService.GetDatabaseStatusAsync();
        
		DatabaseHealthStatusDto dto = new DatabaseHealthStatusDto(health);

		return health.Status == "Healthy" ? Ok(dto) : StatusCode(503, dto);
	}
}