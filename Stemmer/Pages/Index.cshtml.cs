using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stemmer.preprocessing;
using System.Data.SQLite;
using static Stemmer.preprocessing.DBProcess;
namespace Stemmer.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;

        public SearchModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        public void onGet()
        {
      
        }

        [BindProperty]
        public string? input { get; set; }


    }
}

