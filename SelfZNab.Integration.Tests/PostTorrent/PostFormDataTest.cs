using System.Net;
using System.Net.Http.Json;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFluent;
using SelfZNab.Domain.Models;
using SelfZNab.Integration.Tests.Helpers;
using SelfZNab.SharedInTests;
using SharedInTests;

namespace SelfZNab.Integration.Tests.PostTorrent;

[Collection("WithDatabase")]
public class PostFormDataTest
{
    private readonly DotnetApiWebApplicationFactory _webAppFactory;
    private readonly HttpClient _client;
    private readonly Faker<TorrentFormData> _fakerTorrentFormData;

    public PostFormDataTest(DatabaseFixture fixture)
    {
        _webAppFactory = new DotnetApiWebApplicationFactory(
            fixture.Databases[TestContext.PostIntegration].GetConnectionString()
        );
        _client = _webAppFactory.CreateClient();
        _fakerTorrentFormData = Fakers.FakerTorrentFormData();
    }

    [Fact]
    public async Task PostAsync_ShouldReturnResponseWithStatusCode201_WhenPayloadIsValid()
    {
        //Arrange
        var expected = _fakerTorrentFormData.Generate();

        // Act
        var result = await _client.PostAsync("/api/torrents", expected.ToMultipart());

        // Assert
        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.Created);
        Check.That(result.Content).IsNotNull();
        //var actual = await result.Content.ReadFromJsonAsync<TorrentResponse>();
        //Check.That(actual).HasFieldsWithSameValues(expected.ToExpectedDto(1));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(
        "This title is way too long...................................................................................................................................................................................................................................................................................................................................................................................................................................................."
    )]
    public async Task PostAsync_ShouldReturnBadRequest_WhenTitleIsNotValid(string title)
    {
        //Arrange
        var expected = _fakerTorrentFormData.RuleFor(a => a.Title, _ => title).Generate();

        //Act
        var result = await _client.PostAsync("/api/torrents", expected.ToMultipart());

        //Assert
        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Check.That(error).IsNotNull();
        Check.That(error.Errors).ContainsKey("Title");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(9999)]
    [InlineData(-2000)]
    public async Task PostAsync_ShouldReturnBadRequest_WhenCategoryIsNotValid(int cat)
    {
        //Arrange
        var fakeRequest = _fakerTorrentFormData
            .RuleFor(f => f.Type, _ => (Torrent.Category)cat)
            .Generate();

        //Act
        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        //Assert
        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Check.That(error).IsNotNull();
        Check.That(error.Errors).ContainsKey("Type");
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task PostAsync_ShouldReturnBadRequest_WhenCatalodIdIsNotValid(int id)
    {
        //Arrange
        var fakeRequest = _fakerTorrentFormData.RuleFor(f => f.CatalogId, _ => id).Generate();

        //Act
        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        //Assert
        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Check.That(error).IsNotNull();
        Check.That(error.Errors).ContainsKey("CatalogId");
    }

    [Fact]
    public async Task PostAsync_ShouldReturnBadRequest_WhenSeasonIsNotValid()
    {
        //Arrange
        var fakeRequest = _fakerTorrentFormData.RuleFor(f => f.Season, _ => -1).Generate();

        //Act
        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        //Assert
        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Check.That(error).IsNotNull();
        Check.That(error.Errors).ContainsKey("Season");
    }

    [Fact]
    public async Task PostAsync_ShouldReturnBadRequest_WhenEpisodeIsNotValid()
    {
        //Arrange
        var fakeRequest = _fakerTorrentFormData.RuleFor(f => f.Episode, _ => -1).Generate();

        //Act
        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        //Assert
        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Check.That(error).IsNotNull();
        Check.That(error.Errors).ContainsKey("Episode");
    }

    [Fact]
    public async Task PostAsync_ShouldReturnBadRequest_WhenTorrentFileIsEmpty()
    {
        var fakeRequest = _fakerTorrentFormData
            .RuleFor(f => f.TorrentFile, _ => Array.Empty<byte>())
            .Generate();

        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        Check.That(result.Content).IsNotNull();
        //var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        //Check.That(error.Errors).ContainsKey("TorrentFile");
    }

    [Theory]
    [InlineData(50)]
    [InlineData(4_000_000)]
    public async Task PostAsync_ShouldReturnBadRequest_WhenTorrentFileSizeIsInvalid(int byteCount)
    {
        var fakeRequest = _fakerTorrentFormData
            .RuleFor(f => f.TorrentFile, _ => new byte[byteCount])
            .Generate();

        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
        //var error = await result.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        //Check.That(error.Errors).ContainsKey("TorrentFile");
    }

    [Fact]
    public async Task PostAsync_ShouldReturnBadRequest_WhenTorrentFileIsNull()
    {
        var fakeRequest = _fakerTorrentFormData.RuleFor(f => f.TorrentFile, _ => null).Generate();

        var result = await _client.PostAsync("/api/torrents", fakeRequest.ToMultipart());

        Check.That(result).IsNotNull();
        Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
    }
}
