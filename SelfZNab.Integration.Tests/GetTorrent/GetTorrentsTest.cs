using System.Net;
using Microsoft.EntityFrameworkCore;
using NFluent;
using SelfZNab.Domain.Models;
using SelfZNab.Integration.Tests.Seeders;
using SelfZNab.SharedInTests;

namespace SelfZNab.Integration.Tests.GetTorrent;

[Collection("WithDatabase")]
public class GetTorrentsTest
{
    private readonly DotnetApiWebApplicationFactory _webAppFactory;
    private readonly HttpClient _client;
    private readonly Torrent _expectedTorrent;

    public GetTorrentsTest(DatabaseFixture fixture)
    {
        _webAppFactory = new DotnetApiWebApplicationFactory(
            fixture.Databases[TestContext.PostIntegration].GetConnectionString()
        );
        _client = _webAppFactory.CreateClient();
        _expectedTorrent = fixture.GetSeeder<OneTorrentSeeder>(TestContext.GetIntegration).Torrent;
    }

    [Fact]
    public async Task GetAsync_ShouldReturnValidCaps_ForSonarrTCaps()
    {
        //Arrange

        // Act
        var response = await _client.GetAsync("api/torrents?t=caps");

        //Assert
        Check.That(response).IsNotNull();
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        Check.That(content).IsEqualTo(ExpectedXml.GetXml());
        //Check.ThatCode(() => ParseCapabilities(content)).DoesNotThrow();
    }

    [Fact]
    public async Task GetAsync_ShouldReturnTvShows_ForSonarrGenericTvSearch()
    {
        //Arrange

        //Act
        var response = await _client.GetAsync(
            "api/torrents?t=tvsearch&cat=5000,5030,5040,5070,2000&extended=1&offset=0&limit=100"
        );

        //Assert
        Check.That(response).IsNotNull();
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
    }
}
