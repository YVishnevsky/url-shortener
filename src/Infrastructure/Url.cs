namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public class Url
    {
        public string Id { get; private set; }
        public string Value { get; private set; }

        public Url(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
