using System;
using System.Collections.Generic;

namespace FindtheRunningMedian
{
    class Solution
    {
        static double[] RunningMedian(int[] a)
        {
            double[] result = new double[a.Length];

            List<int> aux = new List<int>();

            for (int i = 0; i < a.Length; i++)
            {
                InsertedValue1(aux, a[i]);

                int count = i + 1;
                if (count % 2 == 1)
                    result[i] = aux[count / 2];
                else
                {
                    int n1 = aux[count / 2 - 1];
                    int n2 = aux[count / 2];
                    result[i] = ((double)n1 + (double)n2) / 2;
                }
            }
            return result;
        }

        static void InsertedValue(List<int> aux, int v)
        {
            bool inserted = false;
            for (int j = 0; j < aux.Count; j++)
                if (aux[j] >= v)
                {
                    inserted = true;
                    aux.Insert(j, v);
                    break;
                }
            if (!inserted)
                aux.Add(v);
        }

        static void InsertedValue1(List<int> aux, int v)
        {
            int l = 0, h = aux.Count;

            while (l <= h)
            {
                if (aux.Count == 0)
                {
                    aux.Insert(0, v);
                    break;
                }

                int m = (l + h) / 2;
                if (aux[m] == v)
                {
                    aux.Insert(m, v);
                    break;
                }
                if (v < aux[m])
                {
                    if (m == 0 || v >= aux[m - 1])
                    {
                        aux.Insert(m, v);
                        break;
                    }
                    else
                        h = m - 1;
                }
                else
                {
                    if (m == aux.Count - 1 || v <= aux[m + 1])
                    {
                        aux.Insert(m + 1, v);
                        break;
                    }
                    else
                        l = m + 1;
                }
            }
        }

        static void MainFindtheRunningMedian(string[] args)
        {
            int aCount = Convert.ToInt32(Console.ReadLine());

            int[] a = new int[aCount];

            for (int aItr = 0; aItr < aCount; aItr++)
            {
                int aItem = Convert.ToInt32(Console.ReadLine());
                a[aItr] = aItem;
            }

            double[] result = RunningMedian(a);

            Console.WriteLine(string.Join("\n", result));
        }
    }
}
