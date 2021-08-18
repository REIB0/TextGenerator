using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
      Dictionary<string, string> nextWords,
      string phraseBeginning,
      int wordsCount)
        {
            var phraseBeginningBuilder = new StringBuilder();
            phraseBeginningBuilder.Append(phraseBeginning);
            var phraseBeginningList = phraseBeginning.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < wordsCount; i++)
            {
                string nextWord = "";
                if (phraseBeginningList.Count < 2)
                {
                    if (nextWords.ContainsKey(phraseBeginningList[0]))
                    {
                        phraseBeginningList.Add(nextWords[phraseBeginningList[0]]);
                    }
                }
                else if (phraseBeginningList.Count >= 2)
                {
                    string triGramWordsKey = string.Join(" ", phraseBeginningList[phraseBeginningList.Count - 2], phraseBeginningList[phraseBeginningList.Count - 1]);
                    string biGramWordsKey = phraseBeginningList[phraseBeginningList.Count - 1];
                    if (nextWords.ContainsKey(triGramWordsKey))
                    {
                        phraseBeginningList.Add(nextWords[triGramWordsKey]);
                    }
                    else if (nextWords.ContainsKey(biGramWordsKey))
                    {
                        phraseBeginningList.Add(nextWords[biGramWordsKey]);
                    }
                }

            }
            phraseBeginning = string.Join(" ", phraseBeginningList.ToArray());
            return phraseBeginning;
        }
    }
}