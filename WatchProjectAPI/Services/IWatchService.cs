using WatchProjectAPI.Model;

namespace WatchProjectAPI.Services
{
    public interface IWatchService
    {
        Task<Tuple<bool, string>> SaveData(Watch model);
        Task<IEnumerable<Watch>> GetAll(string? watchName);
       
        Task<IEnumerable<Watch>> GetRandom();
        Task<IEnumerable<Watch>> GetRandom8();
        Task<Watch> GetbyId(Guid id);
        Task Update(Watch model);
        Task Delete(Guid id);


       
    }
}
