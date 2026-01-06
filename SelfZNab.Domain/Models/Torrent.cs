using SelfZNab.Domain.Exceptions;

namespace SelfZNab.Domain.Models;

public record TvShow
{
    public int? CatalogId { get; set; }
    public int? Season { get; set; }
    public int? Episode { get; set; }
}

public record Movie
{
    public int CatalogId { get; set; }
}

public record Torrent
{
    public enum Category
    {
        Movie = 2000,
        TvShow = 5000,
        TvShowHd = 5030,
        TvShowUhd = 5040,
        Anime = 5070,
    }

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Category Type { get; set; }
    public Movie? Movie { get; set; }
    public TvShow? TvShow { get; set; }
    public byte[] TorrentFile { get; set; } = Array.Empty<byte>();

    public Torrent() { }

    public Torrent(
        string title,
        Category type,
        int? catalogId,
        int? episode,
        int? season,
        byte[] torrentFile
    )
    {
        // Mettre code validation torrentFile AVANT assignation donc jute au dessous

        Title = title;
        Type = type;
        Movie = IsValidMovie(type, catalogId) ? new Movie { CatalogId = catalogId.Value } : null;
        TvShow = IsValidTvShow(type, catalogId, season, episode)
            ? new TvShow
            {
                CatalogId = catalogId,
                Season = season,
                Episode = episode,
            }
            : null;
        ;
        TorrentFile = IsValid(torrentFile);
    }

    private bool IsValidMovie(Category t, int? id) => t == Torrent.Category.Movie && id is not null;

    private bool IsValidTvShow(Category t, int? id, int? season, int? ep) =>
        t == Torrent.Category.TvShow && (id is not null || season is not null || ep is not null);

    //
    private byte[] IsValid(byte[] torrentFile)
    {
        if (torrentFile.Length < 51 || torrentFile.Length > 3_999_999)
            throw new TorrentFileSizeException(TorrentFile.Length);
        return torrentFile;
    }
}
