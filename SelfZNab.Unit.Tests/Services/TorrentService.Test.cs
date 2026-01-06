using Bogus;
using NFluent;
using NSubstitute;
using SelfZNab.Domain.Models;
using SelfZNab.Domain.Repositories;
using SelfZNab.Domain.Services;
using SharedInTests;

namespace SelfZNab.Unit.Tests.Services;

public class TorrentServiceTest
{
    private readonly ITorrentRepository _torrentRepository;
    private readonly TorrentService _torrentService;
    private readonly Faker<Torrent> _fakerTorrent;
    private readonly Faker<Movie> _fakerMovie;
    private readonly Faker<TvShow> _fakerTvShow;

    public TorrentServiceTest()
    {
        _torrentRepository = Substitute.For<ITorrentRepository>();
        _torrentService = new TorrentService(_torrentRepository);
        _fakerTorrent = Fakers.FakerTorrent();
        _fakerMovie = Fakers.FakerMovie();
        _fakerTvShow = Fakers.FakerTvShow();
    }

    public class CreateTest : TorrentServiceTest
    {
        [Fact]
        public async Task Create_ShouldReturnCreatedTorrent_WhenTorrentIsValid()
        {
            //Arrange
            var expected = _fakerTorrent.Generate();
            _torrentRepository.Add(Arg.Any<Torrent>()).Returns(expected);

            // Act
            var result = _torrentService.Create(expected);

            //Assert
            Check.That(result).IsNotNull().And.HasFieldsWithSameValues(expected);
        }

        //[Theory]
        //[InlineData(Torrent.Category.Movie)]
        //[InlineData(Torrent.Category.TvShow)]
        //public void CreateAsync_ShouldReturn400_WhenTypeIsInvalid(Torrent.Category type)
        //{
        //    // Arrange
        //    var expected = _fakerTorrent
        //        .RuleFor(x => x.Type, _ => type)
        //        .RuleFor(
        //            x => x.Movie,
        //            type == Torrent.Category.Movie ? null : _fakerMovie.Generate()
        //        )
        //        .RuleFor(
        //            x => x.TvShow,
        //            type == Torrent.Category.TvShow ? null : _fakerTvShow.Generate()
        //        )
        //        .Generate();

        //    // Act & Assert
        //    Check
        //        .ThatCode(() => _torrentService.Create(expected))
        //        .Throws<ArgumentException>()
        //        .WithMessage("type and content mismatch");
        //}

        //[InlineData(null)]
        //[InlineData("")]
        //[InlineData(" ")]
        //[InlineData(
        //    @"This title is way too long.............................
        //        ..................................................
        //        .................................................
        //        ..............................................
        //        ..............................................
        //        ...............................................
        //        ...............................................
        //        ...............................................
        //        ...............................................
        //        ............................"
        //)]
        //public void CreateAsync_ShouldReturn400_WhenTitleIsInvalid(string title)
        //{
        //    // Arrange
        //    var expected = _fakerTorrent.RuleFor(x => x.Title, _ => title).Generate();

        //    // Act & Assert
        //    Check
        //        .ThatCode(() => _torrentService.Create(expected))
        //        .Throws<ArgumentException>()
        //        .WithMessage("Invalid Title");
        //}
    }
}
