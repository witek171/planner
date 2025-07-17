using Common.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace PlannerNet.Controllers
{
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
        public IActionResult GetApplicationHealth()
        {
            var health = _healthCheckService.GetApplicationHealth();

            return StatusCode(health.Status == "Healthy" ? 200 : 503, health);
        }

        [HttpGet("database")]
        public async Task<IActionResult> GetDatabaseHealthAsync()
        {
            var health = await _healthCheckService.GetDatabaseHealthAsync();

            return StatusCode(health.Status == "Healthy" ? 200 : 503, health);
        }
    }
}