using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stemmer.preprocessing;
using static Stemmer.preprocessing.LexicalAnalyzer;
using static Stemmer.preprocessing.StopWordRemover;
using static Stemmer.preprocessing.AmharicStemmer;
namespace Stemmer.Pages
{
    public class ResultModel : PageModel
    {
        [BindProperty]
        public string? input { get; set; }
        public void OnGet(string input)
        {
            this.input = input;
            this.lexicalyAnalyzed = LexicalAnalysis(input);
            this.lexicalyAnalyzed = variantConverter(lexicalyAnalyzed);
            this.StopWordRemoved = RemoveStopwords(lexicalyAnalyzed);
            string[] splitString = StopWordRemoved.Split(' ');

            Stemmed = "";
            foreach (string s in splitString)
            {
                Stemmed += Stem(s)+" ";
            }
        }
        [BindProperty]
        public string? lexicalyAnalyzed { get; set; }
        [BindProperty]
        public string? StopWordRemoved { get; set; }
        [BindProperty]
        public string? Stemmed { get; set; }
    }
}
