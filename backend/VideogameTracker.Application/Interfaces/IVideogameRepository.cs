using VideogameTracker.Domain.Entities;

namespace VideogameTracker.Application.Interfaces;

public interface IVideogameRepository 
{
    Task<IEnumerable<VideogameGlobal>> GetAllAsync();
    Task<VideogameGlobal> AddAsync(VideogameGlobal videogame); // Agregamos el contrato para el POST
}
