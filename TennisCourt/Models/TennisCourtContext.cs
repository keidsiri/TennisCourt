using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TennisCourt.Models
{
  public class TennisCourtContext : IdentityDbContext<ApplicationUser>
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