using SelfZNab.Domain.Models;

namespace SelfZNab.Domain.Repositories;

public interface ITorrentRepository
{
    public Torrent Add(Torrent torrent);

    //private readonly ApplicationDbContext _context;

    //public CommentRepository(ApplicationDbContext context)
    //{
    //    _context = context;
    //}

    //public Comment Add(Comment comment)
    //{
    //    _context.Comments.Add(comment);
    //    _context.SaveChanges();

    //    return comment;
    //}
}
