using WatchProjectAPI.Model;

namespace WatchProjectAPI.Repositories
{
    public interface IWatchRepository
    {
        Task <IEnumerable<Watch>> GetAll();
        Task <IEnumerable<Watch>> GetRandom();
        Task<IEnumerable<Watch>> GetRandom8();
        Task<IEnumerable<Watch>> GetbyName(string? watchName);
        Task<Watch> GetbyId(Guid id);
        Task SaveData(Watch model);
        Task Update(Watch model);
        Task Delete(Guid id);
    }
}
