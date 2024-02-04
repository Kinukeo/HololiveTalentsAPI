using HololiveTalentsApi.Models;
using HololiveTalentsAPI.Controllers;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace HololiveTalentsApi.Services
{
    public class MockLiveTalentService : ILiveTalentsService
    {
        public MockLiveTalentService()
        {
           
        }

        public Task<GetLiveTalentsModel> Get()
        {
             return Task.FromResult( new GetLiveTalentsModel { IsLive = true, LiveTalentsUrl = "dummyURL" });
        }
    }
}
