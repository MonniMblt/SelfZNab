using Microsoft.EntityFrameworkCore;
using SelfZNab.Domain.Models;

namespace SelfZNab.Infra.Data;

public interface IApplicationDbContext
{
    DbSet<Torrent> Torrents { get; set; }

}
