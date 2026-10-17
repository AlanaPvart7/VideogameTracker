using Microsoft.EntityFrameworkCore;
using VideogameTracker.Application.Interfaces;
using VideogameTracker.Domain.Entities;
using VideogameTracker.Infrastructure.Data;

namespace VideogameTracker.Infrastructure.Repositories;

public class VideogameRepository : IVideogameRepository
{
    private readonly VideogameTrackerDbContext _context;

    public VideogameRepository(VideogameTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VideogameGlobal>> GetAllAsync()
    {
        return await _context.VideogamesGlobales.ToListAsync();
    }

    public async Task<VideogameGlobal?> GetByIdAsync(int id)
    {
        return await _context.VideogamesGlobales.FindAsync(id);
    }

    public async Task<VideogameGlobal> AddAsync(VideogameGlobal videogame)
    {
        _context.VideogamesGlobales.Add(videogame);
        await _context.SaveChangesAsync();
        return videogame;
    }

    public async Task UpdateAsync(VideogameGlobal videogame)
    {
        _context.VideogamesGlobales.Update(videogame);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(VideogameGlobal videogame)
    {
        _context.VideogamesGlobales.Remove(videogame);
        await _context.SaveChangesAsync();
    }
}