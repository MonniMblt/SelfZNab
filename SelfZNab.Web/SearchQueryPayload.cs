namespace SelfZNab.Web;

public record SearchQueryPayload
{
    public string Cat { get; set; }
    public string? Q { get; init; }
    public int? Season { get; init; }
    public int? Ep { get; init; }
    public int? TvdbId { get; init; }
}
