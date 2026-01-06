using SelfZNab.Domain.Models;
using SelfZNab.Web.Dtos;

namespace SelfZNab.SharedInTests;

public static class ExpectedTorrent
{
    public static TorrentResponse ToExpectedDto(this TorrentFormData creatableDto, int id) =>
        new TorrentResponse()
        {
            Id = id,
            Title = creatableDto.Title,
            Type = creatableDto.Type,
            Movie =
                creatableDto.Type == Torrent.Category.Movie && creatableDto.CatalogId is not null
                    ? new Movie { CatalogId = creatableDto.CatalogId.Value }
                    : null,
            TvShow =
                creatableDto.Type == Torrent.Category.TvShow
                    ? new TvShow
                    {
                        CatalogId = creatableDto.CatalogId,
                        Season = creatableDto.Season,
                        Episode = creatableDto.Episode,
                    }
                    : null,
        };

    public static Torrent ToExpectedTorrent(this Torrent torrent, int id)
    {
        return new Torrent()
        {
            Id = id,
            Title = torrent.Title,
            Type = torrent.Type,
            Movie = torrent.Movie,
            TvShow = torrent.TvShow,
            TorrentFile = torrent.TorrentFile,
        };
    }

    public static TorrentResponse ToExpectedDto(this Torrent torrent, int id) =>
        new TorrentResponse()
        {
            Id = id,
            Title = torrent.Title,
            Type = torrent.Type,
            Movie = torrent.Movie,
            TvShow = torrent.TvShow,
        };

    public static async Task<Torrent> ToExpectedDomainAsync(this TorrentPayload creatableDto)
    {
        await using var ms = new MemoryStream();
        await creatableDto.TorrentFile.CopyToAsync(ms);

        return new Torrent
        {
            Title = creatableDto.Title,
            Type = creatableDto.Type,
            Movie =
                creatableDto.Type == Torrent.Category.Movie && creatableDto.CatalogId is not null
                    ? new Movie { CatalogId = creatableDto.CatalogId.Value }
                    : null,
            TvShow =
                creatableDto.Type == Torrent.Category.TvShow
                && (
                    creatableDto.CatalogId is not null
                    || creatableDto.Season is not null
                    || creatableDto.Episode is not null
                )
                    ? null
                    : new TvShow
                    {
                        CatalogId = creatableDto.CatalogId,
                        Season = creatableDto.Season,
                        Episode = creatableDto.Episode,
                    },
            TorrentFile = ms.ToArray(),
        };
    }

    public static TorrentResponse ToExpectedDto(this Torrent torrent) =>
        new TorrentResponse(torrent);

    public static TorrentResponse ToExpectedDto(this TorrentPayload payload) =>
        new TorrentResponse()
        {
            Title = payload.Title,
            Type = payload.Type,
            Movie =
                payload.Type == Torrent.Category.Movie && payload.CatalogId is not null
                    ? new Movie { CatalogId = payload.CatalogId.Value }
                    : null,
            TvShow =
                payload.Type == Torrent.Category.TvShow
                && (
                    payload.CatalogId is not null
                    || payload.Season is not null
                    || payload.Episode is not null
                )
                    ? null
                    : new TvShow
                    {
                        CatalogId = payload.CatalogId,
                        Season = payload.Season,
                        Episode = payload.Episode,
                    },
        };
}
