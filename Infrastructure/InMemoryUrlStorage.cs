using System.Collections.Concurrent;

namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public class InMemoryUrlStorage : IUrlStorage
    {
        private static readonly ConcurrentDictionary<string, Url> _storage = new();

        public void Add(Url url)
        {
            if (!_storage.TryAdd(url.Id, url))
                throw new Exception("An error occurred while attempting to add new URL item");
        }

        public IEnumerable<Url> GetAll() => _storage.Values;

        public Url? GetOne(string id) => _storage.ContainsKey(id) ? _storage[id] : null;
    }
}
