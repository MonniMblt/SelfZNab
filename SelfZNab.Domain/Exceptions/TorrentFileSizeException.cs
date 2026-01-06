namespace SelfZNab.Domain.Exceptions;

public class TorrentFileSizeException : ArgumentException
{
    public TorrentFileSizeException(int size)
        : base($"TorrentFile is of invalid size : {size} bytes") { }
}
