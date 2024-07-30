using System.Text.RegularExpressions;

namespace Stemmer.preprocessing
{
    public class AmharicStemmer
    {
        private static readonly Dictionary<char, string> LookupTable = new Dictionary<char, string>
    {
    {'ሀ', "he"},
    {'ሁ', "hu"},
    {'ሂ', "hi"},
    {'ሃ', "ha"},
    {'ሄ', "hE"},
    {'ህ', "h"},
    {'ሆ', "ho"},
    {'ለ', "le"},
    {'ሉ', "lu"},
    {'ሊ', "li"},
    {'ላ', "la"},
    {'ሌ', "lE"},
    {'ል', "l"},
    {'ሎ', "lo"},
    {'ሏ', "lWa"},
    {'ሐ', "He"},
    {'ሑ', "Hu"},
    {'ሒ', "Hi"},
    {'ሓ', "Ha"},
    {'ሔ', "HE"},
    {'ሕ', "H"},
    {'ሖ', "Ho"},
    {'ሗ', "HWa"},
    {'መ', "me"},
    {'ሙ', "mu"},
    {'ሚ', "mi"},
    {'ማ', "ma"},
    {'ሜ', "mE"},
    {'ም', "m"},
    {'ሞ', "mo"},
    {'ሟ', "mWa"},
    {'ሠ', "Se"},
    {'ሡ', "Su"},
    {'ሢ', "Si"},
    {'ሣ', "Sa"},
    {'ሤ', "SE"},
    {'ሥ', "S"},
    {'ሦ', "So"},
    {'ሧ', "SWa"},
    {'ረ', "re"},
    {'ሩ', "ru"},
    {'ሪ', "ri"},
    {'ራ', "ra"},
    {'ሬ', "rE"},
    {'ር', "r"},
    {'ሮ', "ro"},
    {'ሯ', "rWa"},
    {'ሰ', "se"},
    {'ሱ', "su"},
    {'ሲ', "si"},
    {'ሳ', "sa"},
    {'ሴ', "sE"},
    {'ስ', "s"},
    {'ሶ', "so"},
    {'ሷ', "sWa"},
    {'ሸ', "xe"},
    {'ሹ', "xu"},
    {'ሺ', "xi"},
    {'ሻ', "xa"},
    {'ሼ', "xE"},
    {'ሽ', "x"},
    {'ሾ', "xo"},
    {'ሿ', "xWa"},
    {'ቀ', "qe"},
    {'ቁ', "qu"},
    {'ቂ', "qi"},
    {'ቃ', "qa"},
    {'ቄ', "qE"},
    {'ቅ', "q"},
    {'ቆ', "qo"},
    {'ቈ', "qWe"},
    {'ቊ', "qWi"},
    {'ቋ', "qWa"},
    {'ቌ', "qWE"},
    {'ቍ', "qW"},
    {'በ', "be"},
    {'ቡ', "bu"},
    {'ቢ', "bi"},
    {'ባ', "ba"},
    {'ቤ', "bE"},
    {'ብ', "b"},
    {'ቦ', "bo"},
    {'ቧ', "bWa"},
    {'ቨ', "ve"},
    {'ቩ', "vu"},
    {'ቪ', "vi"},
    {'ቫ', "va"},
    {'ቬ', "vE"},
    {'ቭ', "v"},
    {'ቮ', "vo"},
    {'ቯ', "vWa"},
    {'ተ', "te"},
    {'ቱ', "tu"},
    {'ቲ', "ti"},
    {'ታ', "ta"},
    {'ቴ', "tE"},
    {'ት', "t"},
    {'ቶ', "to"},
    {'ቷ', "tWa"},
    {'ቸ', "ce"},
    {'ቹ', "cu"},
    {'ቺ', "ci"},
    {'ቻ', "ca"},
    {'ቼ', "cE"},
    {'ች', "c"},
    {'ቾ', "co"},
    {'ቿ', "cWa"},
    {'ኀ', "hhe"},
    {'ኁ', "hhu"},
    {'ኂ', "hhi"},
    {'ኃ', "hha"},
    {'ኄ', "hhE"},
    {'ኅ', "hh"},
    {'ኆ', "hho"},
    {'ኈ', "hWe"},
    {'ኊ', "hWi"},
    {'ኋ', "hWa"},
    {'ኌ', "hWE"},
    {'ኍ', "hW"},
    {'ነ', "ne"},
    {'ኑ', "nu"},
    {'ኒ', "ni"},
    {'ና', "na"},
    {'ኔ', "nE"},
    {'ን', "n"},
    {'ኖ', "no"},
    {'ኗ', "nWa"},
    {'ኘ', "Ne"},
    {'ኙ', "Nu"},
    {'ኚ', "Ni"},
    {'ኛ', "Na"},
    {'ኜ', "NE"},
    {'ኝ', "N"},
    {'ኞ', "No"},
    {'ኟ', "NWa"},
    {'አ', "e"},
    {'ኡ', "u"},
    {'ኢ', "i"},
    {'ኣ', "a"},
    {'ኤ', "E"},
    {'እ', "I"},
    {'ኦ', "o"},
    {'ኧ', "ea"},
    {'ከ', "ke"},
    {'ኩ', "ku"},
    {'ኪ', "ki"},
    {'ካ', "ka"},
    {'ኬ', "kE"},
    {'ክ', "k"},
    {'ኮ', "ko"},
    {'ኰ', "kWe"},
    {'ኲ', "kWi"},
    {'ኳ', "kWa"},
    {'ኴ', "kWE"},
    {'ኵ', "kW"},
    {'ኸ', "Ke"},
    {'ኹ', "Ku"},
    {'ኺ', "Ki"},
    {'ኻ', "Ka"},
     {'ኼ', "KE"},
    {'ኽ', "K"},
    {'ኾ', "Ko"},
    {'ዀ', "KWe"},
    {'ዂ', "KWi"},
    {'ዃ', "KWa"},
    {'ዄ', "KWE"},
    {'ዅ', "KW"},
    {'ወ', "we"},
    {'ዉ', "wu"},
    {'ዊ', "wi"},
    {'ዋ', "wa"},
    {'ዌ', "wE"},
    {'ው', "w"},
    {'ዎ', "wo"},
    {'ዐ', "E"},
    {'ዑ', "U"},
    {'ዒ', "I"},
    {'ዓ', "A"},
    {'ዔ', "EE"},
    {'ዕ', "II"},
    {'ዖ', "O"},
    {'ዘ', "ze"},
    {'ዙ', "zu"},
    {'ዚ', "zi"},
    {'ዛ', "za"},
    {'ዜ', "zE"},
    {'ዝ', "z"},
    {'ዞ', "zo"},
    {'ዟ', "zWa"},
    {'ዠ', "Ze"},
    {'ዡ', "Zu"},
    {'ዢ', "Zi"},
    {'ዣ', "Za"},
    {'ዤ', "ZE"},
    {'ዥ', "Z"},
    {'ዦ', "Zo"},
    {'ዧ', "ZWa"},
    {'የ', "ye"},
    {'ዩ', "yu"},
    {'ዪ', "yi"},
    {'ያ', "ya"},
    {'ዬ', "yE"},
    {'ይ', "y"},
    {'ዮ', "yo"},
    {'ደ', "de"},
    {'ዱ', "du"},
    {'ዲ', "di"},
    {'ዳ', "da"},
    {'ዴ', "dE"},
    {'ድ', "d"},
    {'ዶ', "do"},
    {'ዷ', "dWa"},
    {'ጀ', "je"},
    {'ጁ', "ju"},
    {'ጂ', "ji"},
    {'ጃ', "ja"},
    {'ጄ', "jE"},
    {'ጅ', "j"},
    {'ጆ', "jo"},
    {'ጇ', "jWa"},
    {'ገ', "ge"},
    {'ጉ', "gu"},
    {'ጊ', "gi"},
    {'ጋ', "ga"},
    {'ጌ', "gE"},
    {'ግ', "g"},
    {'ጎ', "go"},
    {'ጐ', "gWe"},
    {'ጒ', "gWi"},
    {'ጓ', "gWa"},
    {'ጔ', "gWE"},
    {'ጕ', "gW"},
    {'ጠ', "Te"},
    {'ጡ', "Tu"},
    {'ጢ', "Ti"},
    {'ጣ', "Ta"},
    {'ጤ', "TE"},
    {'ጥ', "T"},
    {'ጦ', "To"},
    {'ጧ', "TWa"},
    {'ጨ', "Ce"},
    {'ጩ', "Cu"},
    {'ጪ', "Ci"},
    {'ጫ', "Ca"},
    {'ጬ', "CE"},
    {'ጭ', "C"},
    {'ጮ', "Co"},
    {'ጯ', "CWa"},
    {'ጰ', "Pe"},
    {'ጱ', "Pu"},
    {'ጲ', "Pi"},
    {'ጳ', "Pa"},
    {'ጴ', "PE"},
    {'ጵ', "P"},
    {'ጶ', "Po"},
    {'ጷ', "PWa"},
    {'ጸ', "SSe"},
    {'ጹ', "SSu"},
    {'ጺ', "SSi"},
    {'ጻ', "SSa"},
    {'ጼ', "SSE"},
    {'ጽ', "SS"},
    {'ጾ', "SSo"},
    {'ጿ', "SSWa"},
    {'ፀ', "SSSe"},
    {'ፁ', "SSSu"},
    {'ፂ', "SSSi"},
    {'ፃ', "SSSa"},
    {'ፄ', "SSSE"},
    {'ፅ', "SSS"},
    {'ፆ', "SSSo"},
    {'ፈ', "fe"},
    {'ፉ', "fu"},
    {'ፊ', "fi"},
    {'ፋ', "fa"},
    {'ፌ', "fE"},
    {'ፍ', "f"},
    {'ፎ', "fo"},
    {'ፏ', "fWa"},
    {'ፐ', "pe"},
    {'ፑ', "pu"},
    {'ፒ', "pi"},
    {'ፓ', "pa"},
    {'ፔ', "pE"},
    {'ፕ', "p"},
    {'ፖ', "po"},
    {'ፗ', "pWa"}

    };

        public static string Transliterate(string word, string lang)
        {
            string transWord = "";

            if (lang == "am")
            {
                foreach (char letter in word)
                {
                    if (LookupTable.ContainsKey(letter))
                    {
                        transWord += LookupTable[letter];
                    }
                }
            }
            else if (lang == "en")
            {
                var tokens = SplitWord(word);
                foreach (string letter in tokens)
                {
                    var enLetter = LookupTable.FirstOrDefault(x => x.Value == letter).Key;
                    if (enLetter != default)
                    {
                        transWord += enLetter;
                    }
                }
            }

            return transWord;
        }

        private static List<string> SplitWord(string word)
        {
            List<string> tokens = new List<string>();
            for (int i = 0; i < word.Length; i += 2)
            {
                if (i + 2 <= word.Length)
                {
                    tokens.Add(word.Substring(i, 2));
                }
                else
                {
                    tokens.Add(word.Substring(i, 1));
                }
            }
            return tokens;
        }
        private static List<string> sfxArr = new List<string>();
        private static List<string> pfxArr = new List<string>();

        // Suffix and prefix lists
        private static string suffixList = "ኦችኣችኧውንንኣ|ኦችኣችህኡ|ኦችኣችኧውን|ኣችኧውንንኣ|ኦችኣችኧው|ኢዕኧልኧሽ|ኦችኣችን|ኣውኢው|ኣችኧውኣል|ችኣት|ችኣችህኡ|ችኣችኧው|ኣልኧህኡ|ኣውኦች|ኣልኧህ|ኣልኧሽ|ኣልችህኡ|ኣልኣልኧች|ብኣችኧውስ|ብኣችኧው|ኣችኧውን|ኣልኧች|ኣልኧን|ኣልኣችህኡ|ኣችህኡን|ኣችህኡ|ኣችህኡት|ውኦችንንኣ|ውኦችን|ኣችኧው|ውኦችኡን|ውኦችኡ|ውንኣ|ኦችኡን|ውኦች|ኝኣንኧትም|ኝኣንኣ|ኝኣንኧት|ኝኣን|ኝኣውም|ኝኣው|ኣውኣ|ብኧትን|ኣችህኡም|ችኣችን|ኦችህ|ኦችሽ|ኦችኡ|ኦችኤ|ኦውኣ|ኦቿ|ችው|ችኡ|ኤችኡ|ንኧው|ንኧት|ኣልኡ|ኣችን|ክኡም|ክኡት|ክኧው|ችን|ችም|ችህ|ችሽ|ችን|ችው|ይኡሽን|ይኡሽ|ውኢ|ኦችንንኣ|ኣውኢ|ብኧት|ኦች|ኦችኡ|ውኦን|ኝኣ|ኝኣውን|ኝኣው|ኦችን|ኣል|ም|ሽው|ክም|ኧው|ውኣ|ትም|ውኦ|ውም|ውን|ንም|ሽን|ኣች|ኡት|ኢት|ክኡ|ኤ|ህ|ሽ|ኡ|ሽ|ክ|ች|ኡን|ን|ም|ንኣ";
        private static string prefixList = "ስልኧምኣይ|ይኧምኣት|ዕንድኧ|ይኧትኧ|ብኧምኣ|ብኧትኧ|ዕኧል|ስልኧ|ምኧስ|ዕይኧ|ይኣል|ስኣት|ስኣን|ስኣይ|ስኣል|ይኣስ|ይኧ|ልኧ|ብኧ|ክኧ|እን|አል|አስ|ትኧ|አት|አን|አይ|ይ|አ|እ";

        public static string Stem(string word)
        {
            // Transliterate the word
            string cvString = Transliterate(word, "am");

            // Prepare suffix array
            string[] sarr = suffixList.Split('|');
            foreach (var suffix in sarr)
            {
                sfxArr.Add(Transliterate(suffix, "am"));
            }
            sfxArr.Add("Wa"); // Special case for ሯ

            // Prepare prefix array
            string[] parr = prefixList.Split('|');
            foreach (var prefix in parr)
            {
                pfxArr.Add(Transliterate(prefix, "am"));
            }

            // Remove suffixes
            foreach (var sfx in sfxArr)
            {
                if (cvString.EndsWith(sfx, StringComparison.OrdinalIgnoreCase))
                {
                    cvString = Regex.Replace(cvString, $"{Regex.Escape(sfx)}$", "", RegexOptions.IgnoreCase);
                    break;
                }
            }

            // Remove prefixes
            foreach (var pfx in pfxArr)
            {
                if (cvString.StartsWith(pfx, StringComparison.OrdinalIgnoreCase))
                {
                    cvString = Regex.Replace(cvString, $"^{Regex.Escape(pfx)}", "", RegexOptions.IgnoreCase);
                    break;
                }
            }

            // Remove infixes
            if (Regex.IsMatch(cvString, @".+([^aeiou])[aeiou]\1[aeiou].?", RegexOptions.IgnoreCase))
            {
                cvString = Regex.Replace(cvString, @"\S\S[^aeiou][aeiou]", cvString.Substring(0, 2), RegexOptions.IgnoreCase);
            }
            else if (Regex.IsMatch(cvString, @"^(.+)a\1$", RegexOptions.IgnoreCase))
            {
                cvString = Regex.Replace(cvString, "a.+", "", RegexOptions.IgnoreCase);
            }

            if (Regex.IsMatch(cvString, "[bcdfghjklmnpqrstvwxyz]{2}e", RegexOptions.IgnoreCase))
            {
                var ccv = Regex.Match(cvString, "[bcdfghjklmnpqrstvwxyz]{2}e", RegexOptions.IgnoreCase).Value;
                cvString = Regex.Replace(cvString, "[bcdfghjklmnpqrstvwxyz]{2}e", $"{ccv[0]}X{ccv[1]}", RegexOptions.IgnoreCase);
            }

            return Transliterate(cvString, "en");
        }
    }

}

