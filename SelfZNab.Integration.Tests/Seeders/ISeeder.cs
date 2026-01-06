using SelfZNab.Infra.Data;

namespace SelfZNab.Integration.Tests.Seeders;

public interface ISeeder
{
    public void Seed(ApplicationDbContext context, bool isFirstMigration);
}
