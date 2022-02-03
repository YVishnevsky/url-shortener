namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public interface IRequestLogger
    {
        void Write(string message);
    }
}
