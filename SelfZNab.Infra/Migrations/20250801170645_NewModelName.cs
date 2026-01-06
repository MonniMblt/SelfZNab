using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfZNab.Infra.Migrations;

/// <inheritdoc />
public partial class NewModelName : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Medias");

        migrationBuilder.CreateTable(
            name: "Torrents",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>(type: "TEXT", nullable: false),
                Type = table.Column<string>(type: "TEXT", nullable: false),
                MvdbId = table.Column<int>(type: "INTEGER", nullable: true),
                TvdbId = table.Column<int>(type: "INTEGER", nullable: true),
                TorrentFile = table.Column<byte[]>(type: "BLOB", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Torrents", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Torrents");

        migrationBuilder.CreateTable(
            name: "Medias",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                MvdbId = table.Column<int>(type: "INTEGER", nullable: true),
                Title = table.Column<string>(type: "TEXT", nullable: false),
                TorrentFile = table.Column<byte[]>(type: "BLOB", nullable: false),
                TvdbId = table.Column<int>(type: "INTEGER", nullable: true),
                Type = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Medias", x => x.Id);
            });
    }
}
