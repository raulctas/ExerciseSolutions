using System;
using System.Collections.Generic;
using System.IO;

namespace Hackerrank
{
    class MakingAnagrams
    {

        /*
         * Complete the 'makingAnagrams' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts following parameters:
         *  1. STRING s1
         *  2. STRING s2
         */

        public static int makingAnagrams(string s1, string s2)
        {
            Dictionary<char, int> charsS1 = GetCharacters(s1);
            Dictionary<char, int> charsS2 = GetCharacters(s2);

            return MinimumNumberOfDeletionsToCreateAnagrams(charsS1, charsS2);
        }

        private static Dictionary<char, int> GetCharacters(string s)
        {
            Dictionary<char, int> chars = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
                if (!chars.ContainsKey(s[i]))
                    chars[s[i]] = 1;
                else
                    chars[s[i]]++;
            return chars;
        }

        private static int MinimumNumberOfDeletionsToCreateAnagrams(Dictionary<char, int> charsS1, Dictionary<char, int> charsS2)
        {
            int result = 0;
            foreach (char a in charsS1.Keys)
            {
                if (charsS2.ContainsKey(a))
                    result += Math.Abs(charsS1[a] - charsS2[a]);
                else
                    result += charsS1[a];
            }
            foreach (char a in charsS2.Keys)
                if (!charsS1.ContainsKey(a))
                    result += charsS2[a];
            return result;
        }
    }

    class SolutionMakingAnagrams
    {
        public static void MainMakingAnagrams(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            string s1 = Console.ReadLine();

            string s2 = Console.ReadLine();

            int result = MakingAnagrams.makingAnagrams(s1, s2);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }

}
