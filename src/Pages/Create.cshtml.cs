using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nix.Tasks.UrlShortener.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Nix.Tasks.UrlShortener.Pages
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class CreateModel : PageModel
    {
        private readonly IUrlStorage _urlStorage;
        private readonly IUniqueStringProvider _uniqueStringProvider;

        public CreateModel(IUrlStorage urlStorage, IUniqueStringProvider uniqueStringProvider)
        {
            _urlStorage = urlStorage;
            _uniqueStringProvider = uniqueStringProvider;
        }

        [BindProperty]
        [DataType(DataType.Url)]
        [Display(Name = "Long URL")]
        public string UrlValue { get; set; }


        public void OnGet()
        {
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newId  = _uniqueStringProvider.GetNewId();
            _urlStorage.Add(new Url(id: newId, value: UrlValue));

            return RedirectToPage("./Index");
        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
