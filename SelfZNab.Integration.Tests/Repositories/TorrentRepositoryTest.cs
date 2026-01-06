using Bogus;
using NFluent;
using SelfZNab.Domain.Models;
using SelfZNab.Domain.Repositories;
using SelfZNab.Infra.Data;
using SelfZNab.Infra.Repositories;
using SelfZNab.SharedInTests;
using SharedInTests;

namespace SelfZNab.Integration.Tests.Repositories;

[Collection("WithDatabase")]
public class TorrentRepositoryTest
{
    private readonly ITorrentRepository _repository;
    private readonly ApplicationDbContext _context;
    private readonly Faker<Torrent> _faker;

    public TorrentRepositoryTest(DatabaseFixture fixture)
    {
        _context = fixture.CreateDbContext(TestContext.PostRepoUnit);

        _repository = new TorrentRepository(_context);
        _faker = Fakers.FakerTorrent();
    }

    public class AddTest : TorrentRepositoryTest
    {
        public AddTest(DatabaseFixture fixture)
            : base(fixture) { }

        [Fact]
        public async Task Add_ShouldReturnCreatedTorrent_WhenTorrentIsValid()
        {
            //Arrange
            var expected = _faker.Generate();

            //Act
            var result = _repository.Add(expected);

            //Assert
            Check.That(result).IsNotNull();
            Check.That(result).IsInstanceOf<Torrent>();
            Check.That(result).HasFieldsWithSameValues(expected.ToExpectedTorrent(1));
        }
    }
}
