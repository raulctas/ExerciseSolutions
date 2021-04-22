using System;
using System.Collections.Generic;

namespace Implementaciones
{
    class LongestIncreaseSubsequence1
    {
        static void MainLongestIncreaseSubsequence1(string[] args)
        {
            List<int> result = new List<int>() { 10, 22, 9, 33, 21, 50, 41, 60 };
            int count = LongestIncreaseSubsequenceRecursive(result);
            int count2 = LongestIncreaseSubsequence(result).Count;
            if (count != count2)
                Console.WriteLine("No coinciden!!!!");
        }
        // It returns the longest increase subsequence and its lenght
        public static List<int> LongestIncreaseSubsequence(List<int> numbers)
        {
            int[] l = new int[numbers.Count]; // L[i]=length of longest increasing subsequence in s1,...,sn that ends in si. L[j]=1+max{L[i] : i<j and si<sj} (where max of an empty set is 0)
            int[] p = new int[numbers.Count]; // P[j] the value of the i that achieved that max
            int max = int.MinValue;
            int indice = p[0];
            for (int j = 0; j < numbers.Count; j++)
            {
                l[j] = 1;
                p[j] = 0;
                for (int i = 0; i < j; i++)
                {
                    if (numbers[i] < numbers[j] && l[i] + 1 > l[j])
                    {
                        l[j] = l[i] + 1;
                        if (l[j] > max)
                        {
                            max = l[j];
                            indice = j;
                        }
                        p[j] = i;
                    }
                }
            }

            List<int> longest_subsequense = new List<int>() { numbers[indice] };
            while (longest_subsequense.Count != max)
            {
                indice = p[indice];
                longest_subsequense.Add(numbers[indice]);
            }
            longest_subsequense.Reverse();
            return longest_subsequense;
        }

        public static int LongestIncreaseSubsequenceRecursive(List<int> numbers)
        {
            int max_long = 1;
            LongestIncreaseSubsequenceRecursive(numbers, numbers.Count, ref max_long);
            return max_long;
        }
        private static int LongestIncreaseSubsequenceRecursive(List<int> numbers, int n, ref int max_long)
        {
            if (n == 1)
                return 1;
            int res, max_ending_here = 1; // length of LIS ending with numbers[n-1]

            /* Recursively get all LIS ending with arr[0], arr[1] ... ar[n-2]. If
            arr[i-1] is smaller than arr[n-1], and max ending with arr[n-1] needs
            to be updated, then update it */
            for (int i = 1; i < n; i++)
            {
                res = LongestIncreaseSubsequenceRecursive(numbers, i, ref max_long);
                if (numbers[i - 1] < numbers[n - 1] && res + 1 > max_ending_here)
                    max_ending_here = res + 1;
            }
            // Compare max_ending_here with the overall max. And update the
            // overall max if needed
            if (max_long < max_ending_here)
                max_long = max_ending_here;

            // Return length of LIS ending with arr[n-1]
            return max_ending_here;
        }
    }
}
