using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            //in this list we should have another list with words
            //one list = one sentence
            var correctSentencesList = new List<List<string>>();
            var sentencesList = new List<string>();
            string[] sentencesListArray;
            char[] sentenceSeparators = { '.', '!', '?', ';', ':', '(', ')' };

            sentencesListArray = text.Split(sentenceSeparators);

            for (int i = 0; i < sentencesListArray.Length; i++)
            {
                if (sentencesListArray[i]=="")
                {
                    continue;
                }
                correctSentencesList.Add(GetListOfWords(sentencesListArray[i]));
            }

            return correctSentencesList;
        }
        public static List<string> GetListOfWords(string sentence)
        {

            var wordBuilder = new StringBuilder();
            wordBuilder.Append(sentence);
            var wordsList = new List<string>();
            for (int i = 0; i < wordBuilder.Length; i++)
            {
                if (Char.IsUpper(wordBuilder[i]))
                {
                    wordBuilder[i] = Char.ToLower(wordBuilder[i]);
                }
                if (!char.IsLetter(wordBuilder[i]))
                {
                    if (wordBuilder[i] != '\'')
                    {
                        wordBuilder.Replace(wordBuilder[i], ' ');
                    }
                }
            }

            wordsList = wordBuilder.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            return wordsList;
        }
    }
}