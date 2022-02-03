namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public interface IUniqueStringProvider
    {
        string GetNewId();
    }
}
