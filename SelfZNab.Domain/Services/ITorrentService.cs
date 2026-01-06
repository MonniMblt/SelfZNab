using SelfZNab.Domain.Models;

namespace SelfZNab.Domain.Services;

public interface ITorrentService
{
    public Torrent Create(Torrent model);
}
