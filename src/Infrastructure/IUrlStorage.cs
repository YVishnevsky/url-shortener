namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public interface IUrlStorage
    {
        void Add(Url url);
        IEnumerable<Url> GetAll();
        Url? GetOne(string id);
    }
}
