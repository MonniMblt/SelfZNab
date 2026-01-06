using System.ComponentModel.DataAnnotations;
using SelfZNab.Domain.Models;

namespace SelfZNab.Web.Dtos;

public class TorrentPayload
{
    [Required(AllowEmptyStrings = false)]
    [MinLength(1)]
    [MaxLength(300)]
    //[RegularExpression(@"^[A-Z][a-z]+$")]
    public string Title { get; set; }

    [Required]
    [EnumDataType(typeof(Torrent.Category))]
    public Torrent.Category Type { get; set; }

    [Range(1, int.MaxValue)]
    public int? CatalogId { get; set; }

    [Range(0, int.MaxValue)]
    public int? Season { get; set; }

    [Range(0, int.MaxValue)]
    public int? Episode { get; set; }

    [Required]
    public IFormFile TorrentFile { get; set; }

    public async Task<Torrent> ToDomainAsync()
    {
        await using var ms = new MemoryStream();
        await TorrentFile.CopyToAsync(ms);
        return new Torrent(Title, Type, CatalogId, Season, Episode, ms.ToArray());
    }
}
