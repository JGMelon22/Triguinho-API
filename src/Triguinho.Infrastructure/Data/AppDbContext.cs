using Microsoft.EntityFrameworkCore;
using Triguinho.Core.Domains.Bets.Entities;
using Triguinho.Core.Domains.Games.Entities;
using Triguinho.Core.Domains.Rounds.Entities;
using Triguinho.Infrastructure.Data.Configuration;

namespace Triguinho.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Bet> Bets { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Round> Rounds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new RoundConfiguration());
    }
}