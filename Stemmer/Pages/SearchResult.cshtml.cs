using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stemmer.preprocessing;
using static Stemmer.preprocessing.DBProcess;
namespace Stemmer.Pages
{
    public class SearchResultModel : PageModel
    {
        [BindProperty]
        public string? input { get; set; }

        public void OnGet(string input)
        {
            this.input = "ጄኔፈር ሎፔዝ በአራተኛ ፍቺዋ ከቤን አፍሌክ ጋር ልትለያይ ነው".Trim();
            DBProcess dBProcess = new DBProcess();
            this.Search = dBProcess.SearchResult();
            if (this.Search == null)
            {
                this.Search = "didn't work";
            }

          
        }
        [BindProperty]
        public string? Search { get; set; }    

    }
}
