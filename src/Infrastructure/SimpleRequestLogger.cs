using System.Collections.Concurrent;

namespace Nix.Tasks.UrlShortener.Infrastructure
{
    public class SimpleRequestLogger : IRequestLogger
    {
        private static readonly BlockingCollection<string> _messageQueue = new ();
        private readonly string _fileName;

        public SimpleRequestLogger(string fileName)
        {
            _fileName = fileName;
            Task.Factory.StartNew(
                ProcessQueue,
                this,
                TaskCreationOptions.LongRunning);
        }
               

        public void Write(string message)
        {
            _messageQueue.Add(message);
        }

        private void ProcessMessageQueue()
        {
            foreach (var message in _messageQueue.GetConsumingEnumerable())
            {
                File.AppendAllLines(_fileName, new[] { message });
            }
        }

        private static void ProcessQueue(object? state)
        {
            if (state is not null)
            {
                var logger = (SimpleRequestLogger)state;
                logger.ProcessMessageQueue();
            }
            else
                throw new ArgumentNullException(nameof(state));
        }
    }
}
