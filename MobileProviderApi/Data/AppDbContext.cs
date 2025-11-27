using Microsoft.EntityFrameworkCore;
using MobileProviderApi.Models;

namespace MobileProviderApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts) { }

    public DbSet<Bill> Bills { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
}
