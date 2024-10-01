using SpotifyAPI.Web;

namespace Resenhando2.Core.Entities.SpotifyEntities
{
    public class SpotifyArtist
    {
        public ExternalUrls ExternalUrls { get; set; }
        public Followers Followers { get; set; }
        public IReadOnlyCollection<string> Genres { get; set; }
        public string? Href { get; set; }
        public string? Id { get; set; }
        public IReadOnlyCollection<Image> Images { get; set; }
        public string? Name { get; set; }
        public int Popularity { get; set; }
        public string? Type { get; set; }
        public string? Uri { get; set; }

        public SpotifyArtist()
        {
            ExternalUrls = new ExternalUrls();
            Followers = new Followers();
            Genres = [];
            Images = [];
        }
    }

    public static class SpotifyArtistExtensions
    {
        public static SpotifyArtist ToArtist(this FullArtist fullArtist)
        {
            return new SpotifyArtist
            {
                Genres = fullArtist.Genres,
                Href = fullArtist.Href,
                Id = fullArtist.Id,
                Name = fullArtist.Name,
                Popularity = fullArtist.Popularity,
                Type = fullArtist.Type,
                Uri = fullArtist.Uri,
                ExternalUrls = new ExternalUrls
                {
                    Spotify = fullArtist.ExternalUrls["spotify"]
                },
                Followers = new Followers
                {
                    Href = fullArtist.Followers.Href,
                    Total = fullArtist.Followers.Total
                },
                Images = fullArtist.Images.Select(image => new Image
                {
                    Url = image.Url,
                    Height = image.Height,
                    Width = image.Width
                }).ToList()
            };
        }

        public static List<SpotifyArtist> ToArtists(this IEnumerable<FullArtist> fullArtists)
        {
            return fullArtists.Select(artist => artist.ToArtist()).ToList();
        }
    }
}