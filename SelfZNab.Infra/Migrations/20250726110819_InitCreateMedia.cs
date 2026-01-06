using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfZNab.Infra.Migrations;

/// <inheritdoc />
public partial class InitCreateMedia : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "MvdbId",
            table: "Medias",
            type: "INTEGER",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Title",
            table: "Medias",
            type: "TEXT",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<byte[]>(
            name: "TorrentFile",
            table: "Medias",
            type: "BLOB",
            nullable: false,
            defaultValue: new byte[0]);

        migrationBuilder.AddColumn<int>(
            name: "TvdbId",
            table: "Medias",
            type: "INTEGER",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Type",
            table: "Medias",
            type: "TEXT",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "MvdbId",
            table: "Medias");

        migrationBuilder.DropColumn(
            name: "Title",
            table: "Medias");

        migrationBuilder.DropColumn(
            name: "TorrentFile",
            table: "Medias");

        migrationBuilder.DropColumn(
            name: "TvdbId",
            table: "Medias");

        migrationBuilder.DropColumn(
            name: "Type",
            table: "Medias");
    }
}
