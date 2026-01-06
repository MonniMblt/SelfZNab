using SelfZNab.Domain.Models;

namespace SelfZNab.Web.Dtos;

public class TorrentResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public Torrent.Category Type { get; set; }

    public Movie? Movie { get; set; }

    public TvShow? TvShow { get; set; }

    public TorrentResponse() { }

    public TorrentResponse(Torrent torrent)
    {
        Id = torrent.Id;
        Title = torrent.Title;
        Type = torrent.Type;
        Movie = torrent.Movie;
        TvShow = torrent.TvShow;
    }
}

//Id = torrent.Id;
//        Title = torrent.Title;
//        Type = torrent.Type;
//        CatalogId =
//            torrent.Type == Torrent.Category.Movie
//                ? torrent.Movie.CatalogId
//                : torrent.TvShow.CatalogId;
//Episode = torrent.TvShow?.Episode;
//        Season = torrent.TvShow?.Season;

// Seeders, leechers, peers
// Lien du torrent dans torrent et extraire infos seeders, leechers, peers
