using HololiveTalentsApi.Models;
using HololiveTalentsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace HololiveTalentsApi.Services
{
    public class LiveTalentsService: ILiveTalentsService
    {
        private readonly ILogger<LiveTalentsController> _logger;

        private readonly HttpClient _httpClient;

        private readonly IConfiguration _config;

        public LiveTalentsService(ILogger<LiveTalentsController> logger, HttpClient httpClient, IConfiguration config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _config = config;
        }


        public async Task<GetLiveTalentsModel> Get()
        {
            LiveTalentsModel? liveTalents = null;
            string errorString;

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://youtube.googleapis.com/youtube/v3/search?part=snippet&channelId={_config["TakaneLuiChannelId"]}&key={_config["YoutubeApiKey"]}&eventType=live&type=video");

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                liveTalents = await response.Content.ReadFromJsonAsync<LiveTalentsModel>();
            }
            else
            {
                errorString = $"There was an error finding LiveTalents: {response.ReasonPhrase}";
                _logger.LogError(errorString);
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

            return liveTalentsResponse;
        }
    }
}
