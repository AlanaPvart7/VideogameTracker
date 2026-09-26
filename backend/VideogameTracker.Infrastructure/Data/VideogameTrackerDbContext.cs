using Microsoft.EntityFrameworkCore;
using VideogameTracker.Domain.Entities;

namespace VideogameTracker.Infrastructure.Data;

public class VideogameTrackerDbContext : DbContext {
    public VideogameTrackerDbContext(DbContextOptions options) : base(options) { }

    public DbSet<VideogameGlobal> VideogamesGlobales { get; set; } = null!;
}