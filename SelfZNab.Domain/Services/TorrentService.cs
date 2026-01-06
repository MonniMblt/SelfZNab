using SelfZNab.Domain.Models;
using SelfZNab.Domain.Repositories;

namespace SelfZNab.Domain.Services
{
    public class TorrentService : ITorrentService
    {
        private readonly ITorrentRepository _repository;

        public TorrentService(ITorrentRepository repository)
        {
            _repository = repository;
        }

        public Torrent Create(Torrent torrent)
        {
            //Faire condition où mismatch entre Type et Movie/ TvShow renvoyer erreur +message : "${Category.Type} must contein a ${Category.Type}

            if (
                torrent.Type == Torrent.Category.Movie && torrent.TvShow is not null
                || torrent.Type == Torrent.Category.TvShow && torrent.Movie is not null
            )
                throw new ArgumentException("type and content mismatch");

            if (string.IsNullOrWhiteSpace(torrent.Title) || torrent.Title.Length > 300)
                throw new ArgumentException("Invalid Title", nameof(torrent.Title));

            return _repository.Add(torrent);
        }
    }
}
