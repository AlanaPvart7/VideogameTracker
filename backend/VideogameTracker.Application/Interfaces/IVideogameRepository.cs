using VideogameTracker.Domain.Entities;

namespace VideogameTracker.Application.Interfaces;

public interface IVideogameRepository
{
    Task<IEnumerable<VideogameGlobal>> GetAllAsync();
    Task<VideogameGlobal?> GetByIdAsync(int id);
    Task<VideogameGlobal> AddAsync(VideogameGlobal videogame);
    Task UpdateAsync(VideogameGlobal videogame);
    Task DeleteAsync(VideogameGlobal videogame);
}