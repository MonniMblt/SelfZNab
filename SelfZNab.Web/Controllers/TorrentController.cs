using Microsoft.AspNetCore.Mvc;
using SelfZNab.Domain.Services;
using SelfZNab.Web.Dtos;
using SelfZNab.Web.WebMappers;

namespace SelfZNab.Web.Controllers;

[ApiController]
[Route("api/torrents")]
public class TorrentController : ControllerBase
{
    private readonly ITorrentService _torrentService;

    public TorrentController(ITorrentService torrentService)
    {
        _torrentService = torrentService;
    }

    [HttpPost]
    [RequestSizeLimit(10_000_000)]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(TorrentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TorrentResponse>> CreateAsync([FromForm] TorrentPayload payload)
    {
        try
        {
            var result = _torrentService.Create(await payload.ToDomainAsync());
            return Created("/api/torrents", result.ToDto());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [RequestSizeLimit(10_000_000)]
    [Produces("application/xml")]
    public async Task<IActionResult> GetAsync(
        [FromQuery(Name = "t")] QueryMode type,
        [FromQuery] SearchQueryPayload payload
    ) =>
        type switch
        {
            QueryMode.Caps => Ok(GetCaps()),
            QueryMode.Search or QueryMode.Movie or QueryMode.TvSearch => await SearchAsync(
                type,
                payload
            ),
            _ => BadRequest($"Unsupported request type"),
        };

    private CapsResponse GetCaps() =>
        CapsResponseBuilder
            .Create()
            .SetUrl("http://localhost:5044/api/torrents")
            .SetLimitsMax(100)
            .AddFeature("search")
            .AddFeature("tv-search")
            .AddFeature("movie-search")
            .AddFeature("anime-search")
            .AddSearchType(p => p.Search, "q")
            .AddSearchType(p => p.TvSearch, "q,season,ep,imdbid,tmdbid,tvdbid")
            .AddSearchType(p => p.MovieSearch, "q,imdbid,tmdbid")
            .AddSearchType(p => p.AnimeSearch, "q,season,ep,anidb")
            .AddCategory(2000, "Movies")
            .AddSubCategory(2010, "HD")
            .AddSubCategory(2020, "UHD")
            .AddSubCategory(2030, "SD")
            .AddSubCategory(2040, "BluRay")
            .AddCategory(5000, "TV")
            .AddSubCategory(5030, "HD")
            .AddSubCategory(5040, "UHD")
            .AddSubCategory(5050, "SD")
            .AddSubCategory(5060, "BluRay")
            .AddCategory(5070, "Anime")
            .Build();

    private async Task<IActionResult> SearchAsync(QueryMode type, SearchQueryPayload searchQuery)
    {
        var t = type;
        var sq = searchQuery;
        return new OkObjectResult(t);
    }
}
