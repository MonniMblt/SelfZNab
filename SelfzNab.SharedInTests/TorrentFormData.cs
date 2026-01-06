using SelfZNab.Domain.Models;

namespace SelfZNab.SharedInTests;

public class TorrentFormData
{
    public string Title { get; set; }
    public Torrent.Category Type { get; set; }
    public int? CatalogId { get; set; }
    public int? Season { get; set; }
    public int? Episode { get; set; }
    public byte[] TorrentFile { get; set; } = Array.Empty<byte>();
}
