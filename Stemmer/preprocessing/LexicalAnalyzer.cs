using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace Stemmer.preprocessing
{
    public class LexicalAnalyzer
    {
        private static readonly Dictionary<string, string> commonAmhAbbreviations = new Dictionary<string, string>
        {
            { "ት/ቤት", "ትምህርት ቤት" },
            { "ት/ርት", "ትምህርት" },
            { "ት/ክፍል", "ትምህርት ክፍል" },
            { "ሃ/አለቃ", "ሃምሳ አለቃ" },
            { "ሃ/ስላሴ", "ሃይለ ስላሴ" },
            { "ደ/ዘይት", "ደብረ ዘይት" },
            { "ደ/ታቦር", "ደብረ ታቦር" },
            { "መ/ር", "መምህር" },
            { "መ/ቤት", "መስሪያ ቤት" },
            { "መ/አለቃ", "መቶ አለቃ" },
            { "ክ/ከተማ", "ክፍለ ከተማ" },
            { "ክ/ሀገር", "ክፍለ ሀገር" },
            { "ወ/ር", "" },
            { "ወ/ሮ", "ወይዘሮ" },
            { "ወ/ሪት", "ወይዘሪት" },
            { "ወ/ስላሴ", "ወልደ ስላሴ" },
            { "ፍ/ስላሴ", "ፍቅረ ስላሴ" },
            { "ፍ/ቤት", "ፍርድ ቤት" },
            { "ጽ/ቤት", "ጽህፈት ቤት" },
            { "ሲ/ር", "" },
            { "ፕ/ር", "ፕሮፌሰር" },
            { "ጠ/ሚንስትር", "ጠቅላይ ሚኒስተር" },
            { "ዶ/ር", "ዶክተር" },
            { "ገ/ገዮርጊስ", "" },
            { "ቤ/ክርስትያን", "ቤተ ክርስትያን" },
            { "ም/ስራ", "" },
            { "ም/ቤት", "ምክር ቤተ" },
            { "ተ/ሃይማኖት", "ተክለ ሃይማኖት" },
            { "ሚ/ር", "ሚኒስትር" },
            { "ኮ/ል", "ኮሎኔል" },
            { "ሜ/ጀነራል", "ሜጀር ጀነራል" },
            { "ብ/ጀነራል", "ብርጋደር ጀነራል" },
            { "ሌ/ኮለኔል", "ሌተናንት ኮለኔል" },
            { "ሊ/መንበር", "ሊቀ መንበር" },
            { "አ/አ", "ኣዲስ ኣበባ" },
            { "ር/መምህር", "ርዕሰ መምህር" },
            { "ፕ/ት", "" },
            { "ዓም", "ዓመተ ምህረት" },
            { "ዓ.ዓ", "ዓመተ ዓለም" },
            { "ኤ/ር", "ኢትዮጵያ ሬድዮ" },
            { "ግ/ቤት", "ገቢ ቤት" },
            { "እ/አ", "እንቁ አደር" },
            { "ህ/ህ", "ህጻናት" },
            { "ት/ክ", "ትምህርት ክፍል" },
            { "ል/መ", "ልዩ መዝገብ" },
            { "እ/ቤት", "እንቁ ቤት" },
            { "እ/እ", "እንግዳ እናት" },
            { "ኢ/ቤት", "ኢህአት ቤት" },
            { "ኢ/አ", "ኢትዮጵያ አውራጃ" }
        };


        public static string LexicalAnalysis(string corpus)
        {
            // Remove abbreviations by replacing them with their expanded forms
            foreach (var entry in commonAmhAbbreviations)
            {
                // Using word boundaries to ensure accurate replacement
                var regex = new Regex(@"\b" + Regex.Escape(entry.Key) + @"\b");
                corpus = regex.Replace(corpus, entry.Value);
            }

            // Remove punctuation and special characters
            corpus = Regex.Replace(corpus, @"[.\?""',/#!$%^&*;:፤።{}=\-_`~()]", " ");

            // Remove numbers and Amharic digits
            corpus = Regex.Replace(corpus, @"[፩-፻0-9]", " ");

            // Replace multiple spaces with a single space
            corpus = Regex.Replace(corpus, @"\s{2,}", " ");

            return corpus.Trim();
        }
        private static readonly Dictionary<char, char> VariantList = new(){
        {'ሐ','ሀ'},
        {'ኀ','ሀ'},
        {'ሃ','ሀ'},
        {'ሓ','ሀ'},
        {'ኃ','ሀ'},
        {'ሑ','ሁ'},
        {'ኁ','ሁ'},
        {'ሒ','ሂ'},
        {'ኂ','ሂ'},
        {'ሔ','ሄ'},
        {'ኄ','ሄ'},
        {'ሕ','ህ'},
        {'ኅ','ህ'},
        {'ሖ','ሆ'},
        {'ኆ','ሆ'},
        {'ሗ','ኋ'},
        {'ሠ','ሰ'},
        {'ሡ','ሱ'},
        {'ሢ','ሲ'},
        {'ሣ','ሳ'},
        {'ሤ','ሴ'},
        {'ሥ','ስ'},
        {'ሦ','ሶ'},
        {'ዐ','አ'},
        {'ኣ','አ'},
        {'ኧ','አ'},
        {'ዑ','ኡ'},
        {'ዒ','ኢ'},
        {'ዓ','አ'},
        {'ዔ','ኤ'},
        {'ዕ','እ'},
        {'ዖ','ኦ'},
    };
        public static string variantConverter(string word)
        {
            string nv = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (VariantList.ContainsKey(word[i]))
                    nv += VariantList[word[i]];
                else
                    nv += word[i];
            }
            return nv;
        }

    }
}
