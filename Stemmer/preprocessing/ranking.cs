namespace Stemmer.preprocessing
{
    public class ranking
    {
        static public List<Document> rankedSearch(string query)
        {
            DBProcess process = new DBProcess();
            List<Document> Documents = process.GetAllDocuments();
            List<Document> rankedDocuments = new();
            List<Dictionary<string, double>> wordCountPerDocument = new();
            Documents.ForEach((i) =>
            {
                wordCountPerDocument.Add(wordCount(i.stems));
            });
            Dictionary<string, double> IDF = InverseDocumentFrequencyCalculate(wordCountPerDocument);
            List<Dictionary<string, double>> tf = new();
            wordCountPerDocument.ForEach((i) =>
            {
                tf.Add(termFrequencyCalculate(i));
            });
            List<Dictionary<string, double>> Composite = new();
            tf.ForEach((i) =>
            {
                Composite.Add(CompositeMeasureCalculate(i, IDF));
            }
            );
            List<double> length = new();
            for (int i = 0; i < Composite.Count; i++)
            {
                length.Add(0);
                foreach (var item in Composite[i].Values)
                {
                    length[i] += item * item;
                }
                length[i] = Math.Sqrt(length[i]);
            }


            double queryDistance = 0;
            Dictionary<string, double> queryWeight = CompositeMeasureCalculate(termFrequencyCalculate(wordCount(query)), IDF);
            foreach (var weight in queryWeight.Values)
            {
                queryDistance += weight * weight;
            }
            queryDistance = Math.Sqrt(queryDistance);

            List<double> dotProduct = new();
            for (int i = 0; i < Composite.Count; i++)
            {
                dotProduct.Add(0);
                foreach (var q in queryWeight.Keys)
                {
                    if (Composite[i].ContainsKey(q))
                        dotProduct[i] += queryWeight[q] * Composite[i][q];
                }
            }

            List<double> similarity = new();
            for (int i = 0; i < Documents.Count; i++)
            {
                similarity.Add(dotProduct[i] / (queryDistance * length[i]));
            }

            var indexedSimilarity = similarity
                       .Select((value, index) => new { Value = value, Index = index })
                       .ToList();

            var sortedIndexedSimilarity = indexedSimilarity
                .OrderByDescending(pair => pair.Value)
                .ToList();

            foreach (var pair in sortedIndexedSimilarity)
            {
                Console.WriteLine(pair.Value);
                if (pair.Value > 0.65)
                    rankedDocuments.Add(Documents[pair.Index]);
                else break;
            }
            return rankedDocuments;
        }
        static public Dictionary<string, double> wordCount(string text)
        {
            string[] words = text.Split(' ');
            Dictionary<string, double> wordCounter = new();
            foreach (var word in words)
            {
                if (wordCounter.ContainsKey(word))
                    wordCounter[word]++;
                else
                    wordCounter[word] = 1;
            }
            return wordCounter;
        }

        static public Dictionary<string, double> termFrequencyCalculate(Dictionary<string, double> wordCount)
        {
            Dictionary<string, double> tf = new();
            double max = wordCount.Values.Max();
            foreach (var word in wordCount.Keys)
            {
                tf[word] = wordCount[word] / max;
            }
            return tf;
        }
        static public Dictionary<string, double> InverseDocumentFrequencyCalculate(List<Dictionary<string, double>> wordCountPerDocument)
        {
            Dictionary<string, double> idf = new();

            foreach (var document in wordCountPerDocument)
            {
                foreach (var word in document)
                {
                    if (idf.ContainsKey(word.Key))
                        idf[word.Key]++;
                    else
                        idf[word.Key] = 1;
                }
            }
            foreach (var word in idf.Keys)
            {
                idf[word] = Math.Log(Convert.ToDouble(wordCountPerDocument.Count) / idf[word]) / Math.Log(2);
            }
            return idf;
        }
        static public Dictionary<string, double> CompositeMeasureCalculate(Dictionary<string, double> tf, Dictionary<string, double> idf)
        {
            Dictionary<string, double> weight = new();
            foreach (var word in tf.Keys)
            {
                if (weight.ContainsKey(word))
                    weight[word] = tf[word] * idf[word];
            }
            return tf;
        }
    }
}

