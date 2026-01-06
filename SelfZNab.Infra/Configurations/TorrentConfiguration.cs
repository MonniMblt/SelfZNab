using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfZNab.Domain.Models;

namespace SelfZNab.Infra.Configurations;

public class TorrentConfiguration : IEntityTypeConfiguration<Torrent>
{
    public void Configure(EntityTypeBuilder<Torrent> builder)
    {
        builder.ToTable("Torrent");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(300);
        builder.Property(t => t.Type).IsRequired();
        builder
            .OwnsOne(
                t => t.Movie,
                m =>
                {
                    m.Property(p => p.CatalogId).IsRequired();
                }
            )
            .Navigation(t => t.Movie)
            .IsRequired(false);
        builder
            .OwnsOne(
                t => t.TvShow,
                tv =>
                {
                    tv.Property(p => p.CatalogId).IsRequired(false);
                    tv.Property(p => p.Season).IsRequired(false);
                    tv.Property(p => p.Episode).IsRequired(false);
                }
            )
            .Navigation(t => t.TvShow)
            .IsRequired(false);
        builder.Property(t => t.TorrentFile).IsRequired();

        //builder.OwnsOne(t => t.Movie);
        //builder.OwnsOne(t => t.TvShow);
    }
}
