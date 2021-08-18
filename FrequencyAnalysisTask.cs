using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var bigramDictionary = GetNGrams(text, 2);
            foreach (var item in GetNGrams(text, 3))
            {
                bigramDictionary.Add(item.Key, item.Value);
            }
            foreach (var startingWord in bigramDictionary)
            {


                int maxFrequent = bigramDictionary[startingWord.Key].OrderByDescending(x => x.Value).FirstOrDefault().Value;
                var mostFrequentNextWord = bigramDictionary[startingWord.Key].Where(x => (x.Value == maxFrequent)).OrderBy(i => i.Key, StringComparer.Ordinal).FirstOrDefault();

                result[startingWord.Key] = mostFrequentNextWord.Key;
            }

            return result;
        }




        public static Dictionary<string, Dictionary<string, int>> GetNGrams(List<List<string>> text, int nGrams)
        {
            var dic = new Dictionary<string, Dictionary<string, int>>();
            int nGramsNumber = nGrams - 1;
            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - nGramsNumber; j++)
                {
                    string startingWord = null;
                    string nextWord = null;
                    for (int k = 0; k < nGramsNumber; k++)
                    {
                        if (k + 1 >= nGramsNumber)
                        {
                            startingWord += text[i][j + k];

                        }
                        else
                        {
                            startingWord += text[i][j + k] + " ";

                        }
                        nextWord = text[i][j + nGramsNumber];

                    }
                    if (dic.ContainsKey(startingWord))
                    {
                        bool keyExists = dic[startingWord].ContainsKey(nextWord);

                        if (keyExists)
                        {
                            dic[startingWord][nextWord] = dic[startingWord][nextWord] + 1;
                        }
                        else
                        {
                            dic[startingWord][nextWord] = 1;
                        }
                    }
                    else
                    {
                        dic[startingWord] = new Dictionary<string, int>();
                        dic[startingWord][nextWord] = 1;
                    }
                }
            }

            return dic;
        }

    }
}