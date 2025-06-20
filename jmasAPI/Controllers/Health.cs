using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Health : ControllerBase
    {
        private readonly DateTime _startTime = DateTime.UtcNow;
        private readonly ApplicationDbContext _dbContext;

        public Health(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Verificar conexión a la base de datos
                var canConnect = await _dbContext.Database.CanConnectAsync();

                return Ok(new
                {
                    status = canConnect ? "Healthy" : "Degraded",
                    database = canConnect ? "Connected" : "Disconnected",
                    version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(),
                    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                    timestamp = DateTime.UtcNow,
                    uptime = DateTime.UtcNow - _startTime,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new
                {
                    status = "Unhealthy",
                    error = ex.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
