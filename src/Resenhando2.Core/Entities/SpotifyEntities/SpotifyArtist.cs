namespace Resenhando2.Core.Entities.SpotifyEntities
{
    public class SpotifyArtist
    {
        public ExternalUrls ExternalUrls { get; set; }
        public Followers Followers { get; set; }
        public IReadOnlyCollection<string> Genres { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public IReadOnlyCollection<Image> Images { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}