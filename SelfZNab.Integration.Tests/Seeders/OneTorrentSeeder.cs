using SelfZNab.Domain.Models;
using SelfZNab.Infra.Data;
using SharedInTests;

namespace SelfZNab.Integration.Tests.Seeders;

public class OneTorrentSeeder : ISeeder
{
    public Torrent Torrent { get; set; }

    public void Seed(ApplicationDbContext context, bool isFirstMigration)
    {
        Torrent = Fakers.FakerTorrent().Generate();
        context.Torrents.AddRange(Torrent);
        context.SaveChanges();
    }
}
