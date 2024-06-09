using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities.Configs;

namespace WebApplication1.Entities;

public class MuzykaDbContext: DbContext
{
    protected MuzykaDbContext()
    {
    }

    public MuzykaDbContext(DbContextOptions<MuzykaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MuzykEfConfiguration).Assembly);
    }
}