using Microsoft.EntityFrameworkCore;
using SelfZNab.Domain.Models;
using SelfZNab.Infra.Configurations;

namespace SelfZNab.Infra.Data;

public class ApplicationDbContext
    : DbContext // This class inherits from "DbContext class" (native class from Entity Framework)
        ,
        IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) // The constructor needs an "options" parameter. This parameter is passed to the base class constructor to execute
    { }

    public DbSet<Torrent> Torrents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TorrentConfiguration());
    }
}
