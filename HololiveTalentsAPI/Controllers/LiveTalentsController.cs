using HololiveTalentsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HololiveTalentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiveTalentsController : ControllerBase
    {

        private readonly ILiveTalentsService _liveTalentsService;

        public LiveTalentsController(ILiveTalentsService liveTalentsService)
        {
            _liveTalentsService = liveTalentsService;
        }

        [HttpGet(Name = "GetLiveTalents")]

        public async Task <IActionResult> Get()
        {
            var liveTalentsResponse = await _liveTalentsService.Get();

            return Ok(liveTalentsResponse);
        }
    }
}