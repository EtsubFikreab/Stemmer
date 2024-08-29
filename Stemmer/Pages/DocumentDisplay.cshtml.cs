using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stemmer.preprocessing;

namespace Stemmer.Pages
{
    public class DocumentDisplayModel : PageModel
    {
        [BindProperty]
        public int documentId { get; set; }
        [BindProperty]
        public Document doc { get; set; }
        public void OnGet(int documentId)
        {
            this.documentId = documentId;
            DBProcess db = new DBProcess();
            List<Document> documents = db.GetAllDocuments();
            doc = documents.Where(d => d.primaryKey == documentId).FirstOrDefault();
            doc.body = doc.body.Replace("\\n", string.Empty);

        }
    }
}
