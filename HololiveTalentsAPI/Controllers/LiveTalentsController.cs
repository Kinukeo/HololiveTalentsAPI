using Microsoft.AspNetCore.Mvc;

namespace HololiveTalentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiveTalentsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<LiveTalentsController> _logger;

        public LiveTalentsController(ILogger<LiveTalentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLiveTalents")]
        public IEnumerable<LiveTalents> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new LiveTalents
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}