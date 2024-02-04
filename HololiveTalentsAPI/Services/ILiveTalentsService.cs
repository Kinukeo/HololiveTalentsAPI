using HololiveTalentsApi.Models;

namespace HololiveTalentsApi.Services
{
    public interface ILiveTalentsService
    {
        Task<GetLiveTalentsModel> Get();
    }
}
