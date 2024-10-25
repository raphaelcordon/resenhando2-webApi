using SpotifyAPI.Web;

namespace Resenhando2.Core.Entities.SpotifyEntities
{
    public class SpotifyAlbum(string albumType)
    {
        public string AlbumType { get; private set; } = albumType;
        public int TotalTracks { get; private set; }
        public ExternalUrls ExternalUrls { get; private set; } = new();
        public string? Href { get; private set; }
        public string? Id { get; private set; }
        public IReadOnlyCollection<Image> Images { get; private set; } = new List<Image>();
        public string? Name { get; private set; }
        public string? ReleaseDate { get; private set; }
        public string? ReleaseDatePrecision { get; private set; }
        public string? Type { get; private set; }
        public string? Uri { get; private set; }
        public IReadOnlyCollection<SpotifyArtist> Artists { get; private set; } = new List<SpotifyArtist>();
        public IReadOnlyCollection<string> Genres { get; private set; } = new List<string>();
        public string? Label { get; private set; }
        public int Popularity { get; private set; }


        public static SpotifyAlbum CreateFullAlbum(FullAlbum fullAlbum)
        {
            return new SpotifyAlbum(fullAlbum.AlbumType)
            {
                TotalTracks = fullAlbum.TotalTracks,
                ExternalUrls = new ExternalUrls
                {
                    Spotify = fullAlbum.ExternalUrls["spotify"]
                },
                Href = fullAlbum.Href,
                Id = fullAlbum.Id,
                Images = fullAlbum.Images.Select(image => new Image
                {
                    Url = image.Url,
                    Height = image.Height,
                    Width = image.Width
                }).ToList(),
                Name = fullAlbum.Name,
                ReleaseDate = fullAlbum.ReleaseDate,
                ReleaseDatePrecision = fullAlbum.ReleaseDatePrecision,
                Type = fullAlbum.Type,
                Uri = fullAlbum.Uri,
                Artists = fullAlbum.Artists.Select(artist => new SpotifyArtist
                {
                    Href = artist.Href,
                    Id = artist.Id,
                    Name = artist.Name,
                    Type = artist.Type,
                    Uri = artist.Uri
                }).ToList(),
            };


        }

        public static SpotifyAlbum CreateSimpleAlbum(SimpleAlbum simpleAlbum)
        {
            return new SpotifyAlbum(simpleAlbum.AlbumType)
            {
                TotalTracks = simpleAlbum.TotalTracks,
                ExternalUrls = new ExternalUrls
                {
                    Spotify = simpleAlbum.ExternalUrls["spotify"]
                },
                Href = simpleAlbum.Href,
                Id = simpleAlbum.Id,
                Images = simpleAlbum.Images.Select(image => new Image
                {
                    Url = image.Url,
                    Height = image.Height,
                    Width = image.Width
                }).ToList(),
                Name = simpleAlbum.Name,
                ReleaseDate = simpleAlbum.ReleaseDate,
                ReleaseDatePrecision = simpleAlbum.ReleaseDatePrecision,
                Type = simpleAlbum.Type,
                Uri = simpleAlbum.Uri,
                Artists = simpleAlbum.Artists.Select(artist => new SpotifyArtist
                {
                    Href = artist.Href,
                    Id = artist.Id,
                    Name = artist.Name,
                    Type = artist.Type,
                    Uri = artist.Uri
                }).ToList(),
            };
        }
    };
}
