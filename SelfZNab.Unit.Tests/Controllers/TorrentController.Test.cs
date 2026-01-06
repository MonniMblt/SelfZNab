using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using SelfZNab.Domain.Models;
using SelfZNab.Domain.Services;
using SelfZNab.SharedInTests;
using SelfZNab.Web.Controllers;
using SelfZNab.Web.Dtos;
using SharedInTests;

namespace SelfZNab.Unit.Tests.Controllers;

public class TorrentControllerTest
{
    private readonly ITorrentService _torrentService;
    private readonly TorrentController _torrentController;
    private readonly Faker<TorrentPayload> _fakerCreatableDto;
    private readonly Faker<Torrent> _fakerTorrent;

    public TorrentControllerTest()
    {
        _torrentService = Substitute.For<ITorrentService>();
        _torrentController = new TorrentController(_torrentService);
        _fakerCreatableDto = Fakers.FakerTorrentPayload();
    }

    public class CreateAsyncTest : TorrentControllerTest
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnTorrentDtoWithStatusCode201_WhenTorrentIsValid()
        {
            // Arrange
            var expected = _fakerCreatableDto.Generate();
            _torrentService
                .Create(Arg.Any<Torrent>())
                .Returns(await expected.ToExpectedDomainAsync());

            // Act
            var response = await _torrentController.CreateAsync(expected);

            // Assert
            Check.That(response).IsNotNull().And.IsInstanceOf<ActionResult<TorrentResponse>>();
            Check.That(response.Result).IsNotNull().And.IsInstanceOf<CreatedResult>();
            var result = response.Result as CreatedResult;
            Check.That(result.StatusCode).IsNotNull().And.IsEqualTo(StatusCodes.Status201Created);
            Check.That(result.Value).IsNotNull().And.IsInstanceOf<TorrentResponse>();
            var actual = result.Value as TorrentResponse;
            Check.That(actual).HasFieldsWithSameValues(expected.ToExpectedDto());
        }
    }
}
