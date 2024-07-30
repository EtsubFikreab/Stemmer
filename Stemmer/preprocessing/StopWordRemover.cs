using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Stemmer.preprocessing
{
    public class StopWordRemover
    {
        private static readonly List<string> stopWordList = new List<string>
    {
        "ስለሚሆን", "እና", "ስለዚህ", "በመሆኑም", "ሁሉ", "ሆነ", "ሌላ", "ልክ", "ስለ", "በቀር",
        "ብቻ", "ና", "አንዳች", "አንድ", "እንደ", "እንጂ", "ያህል", "ይልቅ", "ወደ", "እኔ",
        "የእኔ", "ራሴ", "እኛ", "የእኛ", "እራሳችን", "አንቺ", "የእርስዎ", "ራስህ", "ራሳችሁ", "እሱ",
        "እሱን", "የእሱ", "ራሱ", "እርሷ", "የእሷ", "ራሷ", "እነሱ", "እነሱን", "የእነሱ", "እራሳቸው",
        "ምንድን", "የትኛው", "ማንን", "ይህ", "እነዚህ", "እነዚያ", "ነኝ", "ነው", "ናቸው", "ነበር",
        "ነበሩ", "ሁን", "ነበር", "መሆን", "አለኝ", "አለው", "ነበረ", "መኖር", "ያደርጋል", "አደረገው",
        "መሥራት", "ግን", "ከሆነ", "ወይም", "ምክንያቱም", "እስከ", "ቢሆንም", "ጋር",
        "ላይ", "መካከል", "በኩል", "ወቅት", "በኋላ", "ከላይ", "በርቷል", "ጠፍቷል", "በላይ", "ስር",
        "እንደገና", "ተጨማሪ", "ከዚያ", "አንዴ", "እዚህ", "እዚያ", "መቼ", "የት", "እንዴት", "ሁሉም",
        "ማናቸውም", "ሁለቱም", "እያንዳንዱ", "ጥቂቶች", "ተጨማሪ", "በጣም", "ሌላ", "አንዳንድ", "አይ",
        "ወይም", "አይደለም", "ብቻ", "የራስ", "ተመሳሳይ", "ስለዚህ", "እኔም", "ይችላል", "ይሆናል",
        "በቃ", "አሁን"
    };
        public static string RemoveStopwords(string corpus)
        {
            foreach (var word in stopWordList)
            {
                corpus = Regex.Replace(corpus, $@"\b{word}\b", "", RegexOptions.Compiled);
            }

            return corpus;
        }
    }
}
