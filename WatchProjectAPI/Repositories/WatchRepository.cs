using Microsoft.EntityFrameworkCore;
using WatchProjectAPI.Data;
using WatchProjectAPI.Model;

namespace WatchProjectAPI.Repositories
{
    public class WatchRepository : IWatchRepository
    {
        private readonly WatchDbContext _dbcontext;

        public WatchRepository(WatchDbContext dbContext)
        {
            this._dbcontext = dbContext;
        }

        public async Task<IEnumerable<Watch>> GetAll()
        {
            var data = await _dbcontext.Watch.ToListAsync();
            return data;
        }

       
        //add search
        public async Task<IEnumerable<Watch>> GetbyName(string? watchName)
        {
            var data = _dbcontext.Watch.Where(x => x.Name.StartsWith(watchName) || watchName == null).ToList();
            return data;


        }

        public async Task<IEnumerable<Watch>> GetRandom()
        {
            return await _dbcontext.Watch.OrderBy(r => Guid.NewGuid()).Take(4).ToListAsync();
        }
        public async Task<IEnumerable<Watch>> GetRandom8()
        {
            return await _dbcontext.Watch.OrderBy(r => Guid.NewGuid()).Take(8).ToListAsync();
        }

        public async Task<Watch> GetbyId(Guid id)
        {
            return await _dbcontext.Watch.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveData(Watch model) 
        {
            await _dbcontext.Watch.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Update(Watch model)
        {
             _dbcontext.Update(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var watch = _dbcontext.Watch.Find(id);
            if (watch != null)
            {
                _dbcontext.Watch.Remove(watch);
                await _dbcontext.SaveChangesAsync();
            }

        }

    }
}
