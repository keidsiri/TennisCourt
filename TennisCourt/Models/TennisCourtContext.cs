using Microsoft.EntityFrameworkCore;

namespace TennisCourt.Models
{
  public class TennisCourtContext : DbContext
  {
    public DbSet<Court> Courts { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<CourtPlayer> CourtPlayer { get; set; }

    public TennisCourtContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}