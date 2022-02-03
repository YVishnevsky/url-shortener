namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public class SimpleUniqueStringProvider : IUniqueStringProvider
    {
        int _counter;
        readonly string[] _reservedIds;

        public SimpleUniqueStringProvider(uint initialCounter, IEnumerable<string> reservedIds)
        {
            _counter = initialCounter == 0 ? -1 : (int)--initialCounter;
            _reservedIds = reservedIds.Select(s => s.ToLower()).ToArray();
        }

        public string GetNewId()
        {
            string res = GetIdInternal();
            while (_reservedIds.Contains(res.ToLower()))
                res = GetIdInternal();

            return res;
        }

        private string GetIdInternal()
        {
            var counter = Interlocked.Increment(ref _counter);
            return ((uint)counter).ToBase62();
        }
    }
}
