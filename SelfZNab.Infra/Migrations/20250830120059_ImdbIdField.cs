using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfZNab.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ImdbIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MvdbId",
                table: "Torrents",
                newName: "ImdbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImdbId",
                table: "Torrents",
                newName: "MvdbId");
        }
    }
}
