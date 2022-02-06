using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nix.Tasks.UrlShortener.Infrastructure;

namespace Nix.Tasks.UrlShortener.Pages
{
#pragma warning disable CS8618
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUrlStorage _urlStorage;
        
        public IEnumerable<Url> Urls { get; private set; }
        public string Location { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IUrlStorage urlStorage)
        {
            _logger = logger;
            _urlStorage = urlStorage;
        }

        public void OnGet()
        {
            Location = $"{Request.Scheme}://{Request.Host}";
            Urls = _urlStorage.GetAll();
        }
    }
#pragma warning restore CS8618 
}