using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SelfZNab.Infra.Data;
using SelfZNab.Integration.Tests.Seeders;

namespace SelfZNab.Integration.Tests;

public enum TestContext
{
    PostIntegration = 0,
    PostRepoUnit = 1,
    GetIntegration = 2,
    GetRepoUnit = 3,
}

[CollectionDefinition("WithDatabase")]
public class WithDatabaseCollection : ICollectionFixture<DatabaseFixture> { }

public class DatabaseFixture
{
    public Dictionary<TestContext, DatabaseFacade> Databases { get; } = [];
    public Dictionary<TestContext, ISeeder?> Config { get; } =
        new()
        {
            { TestContext.PostIntegration, null },
            { TestContext.PostRepoUnit, null },
            { TestContext.GetIntegration, new OneTorrentSeeder() },
            //{ TestContext.GetRepoUnit, null },
        };

    public DatabaseFixture()
    {
        var basePath = Path.Combine(Environment.CurrentDirectory, "TestDatabases");

        // Only delete files with .db extension
        if (Directory.Exists(basePath))
        {
            foreach (var file in Directory.GetFiles(basePath, "*.db"))
            {
                File.Delete(file);
            }
        }

        Directory.CreateDirectory(basePath);

        foreach (var item in Config)
        {
            var fileName = $"TestDb-{Guid.NewGuid()}.db";
            var dbPath = Path.Combine(basePath, fileName);

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(
                $"Data Source={dbPath}"
            );

            builder = item.Value == null ? builder : builder.UseSeeding(item.Value.Seed);
            var dbContext = new ApplicationDbContext(builder.Options);

            dbContext.Database.Migrate();

            Databases[item.Key] = dbContext.Database;
        }
    }

    public TSeeder GetSeeder<TSeeder>(TestContext key)
        where TSeeder : ISeeder
    {
        if (Config[key] is TSeeder seeder)
        {
            return seeder;
        }

        throw new InvalidOperationException(
            $"Expected {typeof(TSeeder).Name} for key {key}, but found {Config[key]?.GetType().Name ?? "null"}."
        );
    }

    public ApplicationDbContext CreateDbContext(TestContext seederOption) =>
        new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(Databases[seederOption].GetConnectionString())
                .Options
        );
}
