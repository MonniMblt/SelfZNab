using SelfZNab.Domain.Models;
using SelfZNab.Web.Dtos;

namespace SelfZNab.Web.WebMappers;

public static class TorrentMapper
{
    public static TorrentResponse ToDto(this Torrent torrent) => new TorrentResponse(torrent);
}
