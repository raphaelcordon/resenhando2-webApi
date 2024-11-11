using SpotifyAPI.Web;

namespace Resenhando2.Core.Entities.SpotifyEntities
{
    public class SpotifyTrack()
    {
        public string Name { get; private set; }
        public int DiscNumber { get; private set; }
        public int TrackNumber { get; private set; }
        public int DurationMs { get; private set; }
        public bool Explicit { get; private set; }
        public ExternalUrls ExternalUrls { get; private set; } = new();
        public string? Href { get; private set; }
        public string? Id { get; private set; }
        public string? Uri { get; private set; }
        public int Popularity { get; private set; }
        public string? PreviewUrl { get; private set; }
        public IReadOnlyCollection<SpotifyArtist> Artists { get; private set; } = new List<SpotifyArtist>();
        public ExternalIds ExternalIds { get; private set; } = new();
        public IReadOnlyCollection<Image> Images { get; private set; } = new List<Image>(); // Added Images property
        public string? ReleaseDate { get; private set; } // Optional: Include release date if needed
        public string? ReleaseDatePrecision { get; private set; } // Precision of the release date

        public static SpotifyTrack CreateFullTrack(FullTrack fullTrack)
        {
            return new SpotifyTrack()
            {
                Name = fullTrack.Name,
                DiscNumber = fullTrack.DiscNumber,
                TrackNumber = fullTrack.TrackNumber,
                DurationMs = fullTrack.DurationMs,
                Explicit = fullTrack.Explicit,
                ExternalUrls = new ExternalUrls
                {
                    Spotify = fullTrack.ExternalUrls["spotify"]
                },
                Href = fullTrack.Href,
                Id = fullTrack.Id,
                Uri = fullTrack.Uri,
                Popularity = fullTrack.Popularity,
                PreviewUrl = fullTrack.PreviewUrl,
                Artists = fullTrack.Artists.Select(artist => new SpotifyArtist
                {
                    Href = artist.Href,
                    Id = artist.Id,
                    Name = artist.Name,
                    Type = artist.Type,
                    Uri = artist.Uri
                }).ToList(),
                // Conditionally include Album properties if they exist
                Images = fullTrack.Album?.Images.Select(image => new Image
                {
                    Url = image.Url,
                    Height = image.Height,
                    Width = image.Width
                }).ToList() ?? new List<Image>(),
                ReleaseDate = fullTrack.Album?.ReleaseDate,
                ReleaseDatePrecision = fullTrack.Album?.ReleaseDatePrecision,
            };
        }

        // public static SpotifyTrack CreateSimpleTrack(SimpleTrack simpleTrack)
        // {
        //     return new SpotifyTrack()
        //     {
        //         Name = simpleTrack.Name,
        //         DiscNumber = simpleTrack.DiscNumber,
        //         TrackNumber = simpleTrack.TrackNumber,
        //         DurationMs = simpleTrack.DurationMs,
        //         Explicit = simpleTrack.Explicit,
        //         ExternalUrls = new ExternalUrls
        //         {
        //             Spotify = simpleTrack.ExternalUrls["spotify"]
        //         },
        //         Href = simpleTrack.Href,
        //         Id = simpleTrack.Id,
        //         Uri = simpleTrack.Uri,
        //         Artists = simpleTrack.Artists.Select(artist => new SpotifyArtist
        //         {
        //             Href = artist.Href,
        //             Id = artist.Id,
        //             Name = artist.Name,
        //             Type = artist.Type,
        //             Uri = artist.Uri
        //         }).ToList(),
        //         Images = simpleTrack.Album?.Images.Select(image => new Image
        //         {
        //             Url = image.Url,
        //             Height = image.Height,
        //             Width = image.Width
        //         }).ToList() ?? new List<Image>(),
        //         ReleaseDate = fullTrack.Album?.ReleaseDate,
        //         ReleaseDatePrecision = fullTrack.Album?.ReleaseDatePrecision,
        //     };
        // }
    }

    public class ExternalIds
    {
        public string? Isrc { get; set; }
    }
}
