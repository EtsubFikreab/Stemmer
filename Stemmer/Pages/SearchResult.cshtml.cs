using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stemmer.preprocessing;
using System.IO;
using static Stemmer.preprocessing.LexicalAnalyzer;
using static Stemmer.preprocessing.StopWordRemover;
using static Stemmer.preprocessing.AmharicStemmer;
namespace Stemmer.Pages
{
    public class SearchResultModel : PageModel
    {
        [BindProperty]
        public string? input { get; set; }
        [BindProperty]
        public string? documentId { get; set; }
        public void OnPost(string input)
        {
            this.input = input;
            string initialProcessing = RemoveStopwords(variantConverter(LexicalAnalysis(input)));
            string[] splitedString = initialProcessing.Split(' ');
            string stem = "";
            AmharicStemmer.Transliterated transliterated = new();
            foreach (string s in splitedString)
            {
                transliterated.word=Stem(s);
                stem += AmharicStemmer.Transliterate(transliterated, "am").word + " ";
            }

            this.documents= ranking.rankedSearch(stem);
        }
        [BindProperty]
        public List<Document>? documents { get; set; }

    }
}
