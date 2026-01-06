using SelfZNab.Domain.Models;
using SelfZNab.Domain.Repositories;
using SelfZNab.Infra.Data;

namespace SelfZNab.Infra.Repositories;

public class TorrentRepository : ITorrentRepository
{
    private readonly ApplicationDbContext _context;

    public TorrentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Torrent Add(Torrent torrent)
    {
        _context.Torrents.Add(torrent);
        _context.SaveChanges();
        return torrent;
    }
}
