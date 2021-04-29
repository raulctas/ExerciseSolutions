using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hackerrank.Success
{
    class ResultGameOfThronesI
    {

        /*
         * Complete the 'gameOfThrones' function below.
         *
         * The function is expected to return a STRING.
         * The function accepts STRING s as parameter.
         */

        public static string gameOfThrones(string s)
        {
            Dictionary<char, int> charsS = GetCharacters(s);

            return IsAnagramPalindrome(charsS) ? "YES" : "NO";
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

        private static bool IsAnagramPalindrome(Dictionary<char, int> charsS)
        {
            return charsS.Count(c => c.Value % 2 == 1) < 2;
        }
    }

    class SolutionGameOfThronesI
    {
        public static void MainGameOfThronesI(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            string s = Console.ReadLine();

            string result = ResultGameOfThronesI.gameOfThrones(s);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }

}
