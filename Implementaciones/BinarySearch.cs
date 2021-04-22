using System;
using System.Collections.Generic;

namespace Implementaciones
{
    class BinarySearch
    {
        static Random r = new Random();

        static void MainBinarySearch(string[] args)
        {
            int wrong_result = 0;
            int count_test = 10000;//r.Next(1, 1000);

            int true_result = 0;
            int false_result = 0;


            List<int> list;
            int max_value = 1000;
            int count_list;

            int aux = count_test;

            while (aux > 0)
            {
                count_list = r.Next(1, max_value);
                list = CreateList(count_list, max_value);

                int v = r.Next(0, max_value);

                if (Binary_Search(list, v) != Search(list, v))
                    wrong_result++;

                aux--;
            }
            Console.WriteLine(wrong_result + " resultados ìncorrectos de un total de " + count_test);

            List<int> a = new List<int>() { 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 9, 9, 9, 9, 9 };
            Console.WriteLine(Ocurrence(a, 9));
        }

        private static bool Search(List<int> list, int p)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == p)
                    return true;
            return false;
        }
        private static List<int> CreateList(int count, int max_value)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < count; i++)
                result.Add(r.Next(0, max_value));
            result.Sort();
            return result;
        }


        private static bool Binary_Search(List<int> list, int p)
        {
            return Binary_Search(list, p, 0, list.Count - 1);
        }
        private static bool Binary_Search(List<int> list, int p, int a, int b)
        {
            if (a > b)
                return false;

            int centro = (a + b) / 2;

            if (p == list[centro])
                return true;

            if (p < list[centro])
                return Binary_Search(list, p, a, centro - 1);

            return Binary_Search(list, p, centro + 1, b);
        }

        private static int Ocurrence(List<int> list, int p)
        {
            return Binary_Search_LastOcurrence(list, p);
        }

        private static int Binary_Search_LastOcurrence(List<int> list, int p)
        {
            return Binary_Search_LastOcurrence(list, p, 0, list.Count - 1);
        }
        private static int Binary_Search_LastOcurrence(List<int> list, int p, int a, int b)
        {
            if (a > b)
                return -1;


            if (list[b] == p)
                return b;

            if (b - a == 1)
            {
                if (list[a] == p)
                    return a;
            }

            int centro = (a + b) / 2;

            if (p < list[centro])
                return Binary_Search_LastOcurrence(list, p, a, centro - 1);

            return Binary_Search_LastOcurrence(list, p, centro, b);
        }

        private static int Binary_Search_FirstOcurrence(List<int> list, int p)
        {
            return Binary_Search_FirstOcurrence(list, p, 0, list.Count - 1);
        }
        private static int Binary_Search_FirstOcurrence(List<int> list, int p, int a, int b)
        {
            if (a > b)
                return -1;

            if (list[b] == p)
                return b;

            if (b - a == 1)
            {
                if (list[a] == p)
                    return a;
            }

            int centro = (a + b) / 2;

            if (p < list[centro])
                return Binary_Search_LastOcurrence(list, p, centro - 1, b);

            return Binary_Search_LastOcurrence(list, p, a, centro - 1);
        }
    }
}
