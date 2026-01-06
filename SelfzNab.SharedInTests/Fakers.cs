using Bogus;
using Microsoft.AspNetCore.Http;
using SelfZNab.Domain.Models;
using SelfZNab.SharedInTests;
using SelfZNab.Web.Dtos;

namespace SharedInTests;

public static class Fakers
{
    public static Faker<Movie> FakerMovie() =>
        new Faker<Movie>().RuleFor(m => m.CatalogId, f => f.Random.Int(1, int.MaxValue));

    public static Faker<TvShow> FakerTvShow() =>
        new Faker<TvShow>()
            .RuleFor(c => c.CatalogId, f => f.Random.Int(1, int.MaxValue))
            .RuleFor(c => c.Season, f => f.Random.Int(0, int.MaxValue))
            .RuleFor(c => c.Episode, f => f.Random.Int(0, int.MaxValue));

    public static Faker<Torrent> FakerTorrent() =>
        new Faker<Torrent>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
            .RuleFor(c => c.Type, f => f.PickRandom<Torrent.Category>())
            .RuleFor(
                c => c.Movie,
                (f, c) => c.Type == Torrent.Category.Movie ? FakerMovie().Generate() : null
            )
            .RuleFor(
                c => c.TvShow,
                (f, c) => c.Type == Torrent.Category.TvShow ? FakerTvShow().Generate() : null
            )
            .RuleFor(t => t.TorrentFile, f => f.Random.Bytes(f.Random.Int(100, 1024)));

    public static Faker<TorrentResponse> FakerTorrentResponse() =>
        new Faker<TorrentResponse>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
            .RuleFor(c => c.Type, f => f.PickRandom<Torrent.Category>())
            .RuleFor(
                c => c.Movie,
                (f, c) => c.Type == Torrent.Category.Movie ? FakerMovie().Generate() : null
            )
            .RuleFor(
                c => c.TvShow,
                (f, c) => c.Type == Torrent.Category.TvShow ? FakerTvShow().Generate() : null
            );

    public static Faker<TorrentPayload> FakerTorrentPayload() =>
        new Faker<TorrentPayload>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
            .RuleFor(c => c.Type, f => f.PickRandom<Torrent.Category>())
            .RuleFor(c => c.CatalogId, f => f.Random.Int(1, int.MaxValue))
            .RuleFor(c => c.Season, f => f.Random.Int(0, int.MaxValue))
            .RuleFor(c => c.Episode, f => f.Random.Int(0, int.MaxValue))
            .RuleFor(
                c => c.TorrentFile,
                f =>
                {
                    var byteCount = f.Random.Number(200_000, 3_000_000);
                    var randomBytes = f.Random.Bytes(byteCount);

                    var stream = new MemoryStream(randomBytes);
                    return new FormFile(stream, 0, byteCount, "TorrentFile", "fake_movie.torrent")
                    {
                        Headers = new HeaderDictionary
                        {
                            ["Content-Disposition"] =
                                "form-data; name=\"TorrentFile\"; filename=\"fake_movie.torrent\"",
                            ["Content-Type"] = "application/x-bittorrent",
                        },
                        ContentType = "application/x-bittorrent",
                    };
                }
            );

    public static Faker<TorrentFormData> FakerTorrentFormData() =>
        new Faker<TorrentFormData>()
            .RuleFor(c => c.Title, f => f.Lorem.Sentence(3))
            .RuleFor(c => c.Type, f => f.PickRandom<Torrent.Category>())
            .RuleFor(c => c.CatalogId, f => f.Random.Int(1, int.MaxValue))
            .RuleFor(c => c.Season, f => f.Random.Int(0, int.MaxValue))
            .RuleFor(c => c.Episode, f => f.Random.Int(0, int.MaxValue))
            .RuleFor(
                c => c.TorrentFile,
                f =>
                {
                    var byteCount = f.Random.Number(200_000, 3_000_000);
                    return f.Random.Bytes(byteCount);
                }
            );

    public static Faker<TorrentFormData> RuleForTorrentFormDataFromId(
        this Faker<TorrentFormData> formData,
        int id
    )
    {
        return formData.RuleFor(x => x.CatalogId, _ => id);
    }
}
