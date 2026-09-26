using Microsoft.EntityFrameworkCore;
using VideogameTracker.Application.Interfaces;
using VideogameTracker.Domain.Entities;
using VideogameTracker.Infrastructure.Data;

namespace VideogameTracker.Infrastructure.Repositories;


public class VideogameRepository : IVideogameRepository {
    private readonly VideogameTrackerDbContext _context;

    
    public VideogameRepository(VideogameTrackerDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<VideogameGlobal>> GetAllAsync() {
        return await _context.VideogamesGlobales.ToListAsync(); 
    }
}