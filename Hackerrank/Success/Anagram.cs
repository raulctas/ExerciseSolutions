using System;
using System.Collections.Generic;
using System.IO;

namespace Hackerrank
{
    class Anagram
    {

        /*
         * Complete the 'anagram' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts STRING s as parameter.
         */

        public static int anagram(string s)
        {
            if (s.Length % 2 == 1)
                return -1;

            Dictionary<char, int> dif1 = GetCharacters(s.Substring(0, s.Length / 2));

            Dictionary<char, int> dif2 = GetCharacters(s.Substring(s.Length / 2));

            int result = 0;
            foreach (char a in dif1.Keys)
            {
                if (dif2.ContainsKey(a))
                    result += Math.Abs(dif1[a] - dif2[a]);
                else
                    result += dif1[a];
            }
            foreach (char a in dif2.Keys)
                if (!dif1.ContainsKey(a))
                    result += dif2[a];
            return result / 2;
        }

        private static Dictionary<char, int> GetCharacters(string s)
        {
            Dictionary<char, int> chars = new Dictionary<char, int>();
            for (int i = 0; i < s.Length / 2; i++)
                if (!chars.ContainsKey(s[i]))
                    chars[s[i]] = 1;
                else
                    chars[s[i]]++;
            return chars;
        }
    }

    class Solution
    {
        public static void MainAnagram(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int q = Convert.ToInt32(Console.ReadLine().Trim());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string s = Console.ReadLine();

                int result = Anagram.anagram(s);

                textWriter.WriteLine(result);
            }

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
