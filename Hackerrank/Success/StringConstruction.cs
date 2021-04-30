using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hackerrank.Success
{
    class ResultStringConstruction
    {

        /*
         * Complete the 'stringConstruction' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts STRING s as parameter.
         */

        public static int stringConstruction(string s)
        {
            return GetCharactersCount(s).Count;
        }

        private static Dictionary<char, int> GetCharactersCount(string s)
        {
            Dictionary<char, int> chars = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
                if (!chars.ContainsKey(s[i]))
                    chars[s[i]] = 1;
                else
                    chars[s[i]]++;
            return chars;
        }
    }

    class SolutionStringConstruction
    {
        public static void MainStringConstruction(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int q = Convert.ToInt32(Console.ReadLine().Trim());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string s = Console.ReadLine();

                int result = ResultStringConstruction.stringConstruction(s);

                textWriter.WriteLine(result);
            }

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
