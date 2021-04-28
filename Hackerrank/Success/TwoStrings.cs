using System;
using System.Collections.Generic;
using System.IO;

namespace Hackerrank
{
    class ResultTwoStrings
    {

        /*
         * Complete the 'twoStrings' function below.
         *
         * The function is expected to return a STRING.
         * The function accepts following parameters:
         *  1. STRING s1
         *  2. STRING s2
         */

        public static string twoStrings(string s1, string s2)
        {
            Dictionary<char, bool> dictS1 = new Dictionary<char, bool>();
            for (int i = 0; i < s1.Length; i++)
                dictS1[s1[i]] = true;
            for (int i = 0; i < s2.Length; i++)
                if (dictS1.ContainsKey(s2[i]))
                    return "YES";
            return "NO";
        }

    }

    class TwoStrings
    {
        public static void MainTwoStrings(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int q = Convert.ToInt32(Console.ReadLine().Trim());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string s1 = Console.ReadLine();

                string s2 = Console.ReadLine();

                string result = ResultTwoStrings.twoStrings(s1, s2);

                textWriter.WriteLine(result);
            }

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
