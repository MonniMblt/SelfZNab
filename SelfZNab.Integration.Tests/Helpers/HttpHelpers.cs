using System.Net.Http.Headers;
using SelfZNab.SharedInTests;

namespace SelfZNab.Integration.Tests.Helpers;

public static class HttpHelpers
{
    public static MultipartFormDataContent ToMultipart(this TorrentFormData t)
    {
        var form = new MultipartFormDataContent();

        if (t.Title is not null)
            form.Add(new StringContent(t.Title), "Title");

        form.Add(new StringContent(t.Type.ToString()), "Type");

        if (t.CatalogId is not null)
            form.Add(new StringContent(t.CatalogId.Value.ToString()), "CatalogId");

        if (t.Season is not null)
            form.Add(new StringContent(t.Season.Value.ToString()), "Season");

        if (t.Episode is not null)
            form.Add(new StringContent(t.Episode.Value.ToString()), "Episode");

        if (t.TorrentFile is not null)
        {
            var fileContent = new ByteArrayContent(t.TorrentFile);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-bittorrent");

            form.Add(fileContent, "TorrentFile", "fake_movie.torrent");
        }
        return form;
    }
}
