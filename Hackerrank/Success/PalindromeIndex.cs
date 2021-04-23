using System;
using System.IO;

namespace Hackerrank
{
    class Result
    {
        public static int palindromeIndex(string s)
        {
            int start = 0;
            int end = s.Length - 1;
            while (start < end)
            {
                if (s[start] != s[end])
                {
                    if (isPalindrome(s, start + 1, end))
                        return start;
                    if (isPalindrome(s, start, end - 1))
                        return end;
                }
                else
                {
                    start++;
                    end--;
                }
            }
            return -1;
        }

        public static bool isPalindrome(string s, int start, int end)
        {
            while (start < end)
            {
                if (s[start] != s[end])
                    return false;
                start++;
                end--;
            }
            return true;
        }
    }

    class PalindromeIndex
    {
        public static void MainPalindromeIndex(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int q = Convert.ToInt32(Console.ReadLine().Trim());

            for (int qItr = 0; qItr < q; qItr++)
            {
                string s = Console.ReadLine();

                int result = Result.palindromeIndex(s);

                textWriter.WriteLine(result);
            }

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
