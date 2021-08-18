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
            var bigramDictionary = GetNGrams(text, 2);
            //Max nGrams power=3
            AddNGrams(text, 3, bigramDictionary);
            return GetSortedDictionary(bigramDictionary);
        }

        private static Dictionary<string, Dictionary<string, int>> AddNGrams(List<List<string>> text, int maxNGramsPower, Dictionary<string, Dictionary<string, int>> bigramDictionary)
        {
            for (int i = 3; i <= maxNGramsPower; i++)
            {
                foreach (var item in GetNGrams(text, i))
                {
                    bigramDictionary.Add(item.Key, item.Value);
                }
            }
            return bigramDictionary;
        }

        private static Dictionary<string, string> GetSortedDictionary(Dictionary<string, Dictionary<string, int>> bigramDictionary)
        {
            var result = new Dictionary<string, string>();

            foreach (var startingWord in bigramDictionary)
            {

                int maxFrequent = bigramDictionary[startingWord.Key].OrderByDescending(x => x.Value).FirstOrDefault().Value;
                var mostFrequentNextWord = bigramDictionary[startingWord.Key].Where(x => (x.Value == maxFrequent)).OrderBy(i => i.Key, StringComparer.Ordinal).FirstOrDefault();

                result[startingWord.Key] = mostFrequentNextWord.Key;
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, int>> GetNGrams(List<List<string>> text, int nGrams)
        {
            var dic = new Dictionary<string, Dictionary<string, int>>();
            int nGramsNumber = nGrams - 1;
            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - nGramsNumber; j++)
                {
                    CreatingNGrams(text, dic, i, j, nGramsNumber);
                }
            }

            return dic;
        }

        private static void CreatingNGrams(List<List<string>> text, Dictionary<string, Dictionary<string, int>> dic, int i, int j, int nGramsNumber)
        {
            string startingWord;
            string nextWord;

            GetStartAndNextWord(out startingWord, out nextWord, text, i, j, nGramsNumber);
            FillingDictionary(startingWord, nextWord, text, dic);
        }

        private static string GetStartAndNextWord(out string startingWord, out string nextWord, List<List<string>> text, int i, int j, int nGramsNumber)
        {
            startingWord = null;
            nextWord = null;

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
            return nextWord;
        }

        private static void FillingDictionary(string startingWord, string nextWord, List<List<string>> text, Dictionary<string, Dictionary<string, int>> dic)
        {
            bool dicKeyExists = dic.ContainsKey(startingWord);
            if (dicKeyExists)
            {
                bool startingWordKeyExists = dic[startingWord].ContainsKey(nextWord);

                if (startingWordKeyExists)
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
}