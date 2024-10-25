using SpotifyAPI.Web;

namespace Resenhando2.Core.Entities.SpotifyEntities;

public class SpotifyArtistAlbums
{
    public string? Href { get; init; }
    public int? Limit { get; init; }
    public string? Next { get; init; }
    public int? Offset { get; init; }
    public string? Previous { get; init; }
    public IReadOnlyCollection<SpotifyAlbum> Items { get; init; }

    private SpotifyArtistAlbums(string? href, int? limit, string? next, int? offset, string? previous, IReadOnlyCollection<SpotifyAlbum> items)
    {
        Href = href;
        Limit = limit;
        Next = next;
        Offset = offset;
        Previous = previous;
        Items = items;
    }

    public static SpotifyArtistAlbums CreateArtistAlbums(Paging<SimpleAlbum> spotifyPagingAlbums)
    {
        // Convert each SimpleAlbum in the Paging<SimpleAlbum> to SpotifyAlbum using your custom entity
        var items = spotifyPagingAlbums.Items!.Select(SpotifyAlbum.CreateSimpleAlbum).ToList();
        
        return new SpotifyArtistAlbums(
            spotifyPagingAlbums.Href,
            spotifyPagingAlbums.Limit,
            spotifyPagingAlbums.Next,
            spotifyPagingAlbums.Offset,
            spotifyPagingAlbums.Previous,
            items
        );
    }
}
