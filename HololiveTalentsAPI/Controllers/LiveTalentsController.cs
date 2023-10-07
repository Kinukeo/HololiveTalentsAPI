using HololiveTalentsApi.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HololiveTalentsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LiveTalentsController : ControllerBase
    {

        private readonly ILogger<LiveTalentsController> _logger;

        private readonly HttpClient _httpClient;

        public LiveTalentsController(ILogger<LiveTalentsController> logger, HttpClient httpClient)
        {
            _logger = logger;

            _httpClient = httpClient;
        }

        [HttpGet(Name = "GetLiveTalents")]

        public async Task <IActionResult> Get()
        {
            LiveTalentsModel? liveTalents = null;
            string errorString;

            var request = new HttpRequestMessage(HttpMethod.Get, "https://youtube.googleapis.com/youtube/v3/search?part=snippet&channelId=UCSJ4gkVC6NrvII8umztf0Ow&key={APIKeyPlaceholder}&eventType=live&type=video");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                liveTalents = await response.Content.ReadFromJsonAsync<LiveTalentsModel>();
            }
            else
            {
                errorString = $"There was an error finding LiveTalents: {response.ReasonPhrase}";
            }

            GetLiveTalentsModel liveTalentsResponse;

            if (liveTalents?.items.Any() == false) 
            {
                liveTalentsResponse = new GetLiveTalentsModel { IsLive = false };
            }
            else 
            {
                liveTalentsResponse = new GetLiveTalentsModel { IsLive = true, LiveTalentsUrl = $"https://www.youtube.com/watch?v={liveTalents?.items.First().id.videoId}" };
            }

            return Ok(liveTalentsResponse);
        }
    }
}