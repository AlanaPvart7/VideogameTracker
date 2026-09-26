using VideogameTracker.Domain.Entities;

namespace VideogameTracker.Application.Interfaces;

public interface IVideogameRepository {
    Task<IEnumerable<VideogameGlobal>> GetAllAsync();
}