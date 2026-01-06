using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfZNab.Infra.Migrations
{
    /// <inheritdoc />
    public partial class GroupedParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Torrents",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "Torrents");

            migrationBuilder.RenameTable(
                name: "Torrents",
                newName: "Torrent");

            migrationBuilder.RenameColumn(
                name: "Season",
                table: "Torrent",
                newName: "TvShow_Season");

            migrationBuilder.RenameColumn(
                name: "Episode",
                table: "Torrent",
                newName: "TvShow_Episode");

            migrationBuilder.RenameColumn(
                name: "TvdbId",
                table: "Torrent",
                newName: "TvShow_CatalogId");

            migrationBuilder.RenameColumn(
                name: "TmdbId",
                table: "Torrent",
                newName: "Movie_CatalogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Torrent",
                table: "Torrent",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Torrent",
                table: "Torrent");

            migrationBuilder.RenameTable(
                name: "Torrent",
                newName: "Torrents");

            migrationBuilder.RenameColumn(
                name: "TvShow_Season",
                table: "Torrents",
                newName: "Season");

            migrationBuilder.RenameColumn(
                name: "TvShow_Episode",
                table: "Torrents",
                newName: "Episode");

            migrationBuilder.RenameColumn(
                name: "TvShow_CatalogId",
                table: "Torrents",
                newName: "TvdbId");

            migrationBuilder.RenameColumn(
                name: "Movie_CatalogId",
                table: "Torrents",
                newName: "TmdbId");

            migrationBuilder.AddColumn<int>(
                name: "ImdbId",
                table: "Torrents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Torrents",
                table: "Torrents",
                column: "Id");
        }
    }
}
