using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Stemmer.preprocessing.LexicalAnalyzer;
using static Stemmer.preprocessing.StopWordRemover;
using static Stemmer.preprocessing.AmharicStemmer;
using static Stemmer.preprocessing.DBProcess;
using Stemmer.preprocessing;
namespace Stemmer.Pages
{
    public class AddingModel : PageModel
    {
        public string? body { get; set; }
        public string? title { get; set; }

        public string? status { get; set; }


        public void OnGet(string title,string body)
        {
            this.body = body;
            this.lexicalyAnalyzed = LexicalAnalysis(body);
            this.lexicalyAnalyzed = variantConverter(lexicalyAnalyzed);
            this.StopWordRemoved = RemoveStopwords(lexicalyAnalyzed);
            string[] splitString = StopWordRemoved.Split(' ');

            Stemmed = "";
            foreach (string s in splitString)
            {
                Stemmed += Stem(s) + " ";
            }


            this.title = title;
            this.lexicalyAnalyzed = LexicalAnalysis(title);
            this.lexicalyAnalyzed = variantConverter(lexicalyAnalyzed);
            this.StopWordRemoved = RemoveStopwords(lexicalyAnalyzed);
            string[] splitedString = StopWordRemoved.Split(' ');

            stemm = "";
            foreach (string s in splitedString)
            {
                stemm += Stem(s) + " ";
            }
            

            DBProcess dBProcess = new DBProcess();
           if( dBProcess.AddIRdata(title, body, stemm, Stemmed))
            {
                status = "adding successful";
            }
            else
            {
                status = "not successfull";
            }

        }
        [BindProperty]
        public string? lexicalyAnalyzed { get; set; }
        [BindProperty]
        public string? StopWordRemoved { get; set; }
        [BindProperty]
        public string? Stemmed { get; set; }
        [BindProperty]
        public string? stemm { get; set; }
    }
}
