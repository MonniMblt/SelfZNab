using Bogus;
using SelfZNab.Web.Dtos;

namespace SelfZNab.Unit.Tests.Mappers;

public class ToDomainAsyncTest
{
    public readonly Faker<TorrentPayload> _fakerCreatableDto;

    public ToDomainAsyncTest()
    {
        _fakerCreatableDto = new Faker<TorrentPayload>();
    }

    [Fact]
    public async Task IsValidTvShow_ShouldReturnBoolValue_CorrespondingtoTorrentType()
    {
        ////Arrange
        //var creatableDto = _fakerCreatableDto.Generate();
        //var expected = creatableDto.Type == Torrent.Category.Movie;
        ////Act
        //var result = creatableDto.IsValidMovie();

        ////Assert
        //Check.That(result).IsEqualTo(expected);
    }

    //[Theory]
    public async Task CreateMovie_ShouldReturnMovie_WhenTorrentTypeIsMovie()
    {
        ////Arrange
        //var creatableDto = _fakerCreatableDto.Generate();
        //var expected = creatableDto.Type == Torrent.Category.Movie;

        ////Act
        //var result = creatableDto.IsValidMovie();

        ////Assert
        //Check.That(result).IsEqualTo(expected);
    }
}
