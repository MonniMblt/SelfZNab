using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfZNab.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ExtraParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Episode",
                table: "Torrents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Season",
                table: "Torrents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Torrents",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Episode",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Torrents");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Torrents");
        }
    }
}
