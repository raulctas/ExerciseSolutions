using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementaciones
{
    class Subsequence
    {
        static Random r = new Random();

        static void Main(string[] args)
        {
            //Testing one by one.
            //while (true)
            //{
            //    string[] aux = Console.ReadLine().Split(' ');

            //    Console.WriteLine(Subsecuence2(aux[0], aux[1]));
            //}

            int count = 10000;
            int test_count = count;

            long time = 0;
            long time1 = 0;
            long time2 = 0;
            long timeRecursive = 0;

            List<Test> wrong_results = new List<Test>();

            while (count != 0)
            {
                int length_t = r.Next(3, 300);
                string b = "";
                for (int i = 0; i < length_t; i++)
                    b += (char)r.Next(95, 122);

                int length_p = r.Next(1, length_t / 3);
                string a = "";
                for (int i = 0; i < length_p; i++)
                    a += (char)r.Next(95, 122);

                int start_time = Environment.TickCount;
                string result_recursive = SubsecuenceRecursive(a, b);
                timeRecursive += Environment.TickCount - start_time;

                start_time = Environment.TickCount;
                string result = Subsecuence(a, b);
                time += Environment.TickCount - start_time;

                start_time = Environment.TickCount;
                string result1 = Subsecuence1(a, b);
                time1 += Environment.TickCount - start_time;

                start_time = Environment.TickCount;
                string result2 = Subsecuence2(a, b);
                time2 += Environment.TickCount - start_time;

                if (result == result1 && result == result2 && result == result_recursive)
                    Console.WriteLine(result);
                else
                {
                    Console.WriteLine("Subsequence Recursive: " + result_recursive + "  Subsequence: " + result + "  Subsequence1: " + result1 + "  Subsequence2: " + result2 + " Error!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!111");

                    wrong_results.Add(new Test(a, b, result, result1, result2, result_recursive));
                }

                count--;
            }

            Console.WriteLine("Finals Times:");
            Console.WriteLine("Subsequence Recursive Time: " + timeRecursive / 1000 + ":" + timeRecursive % 1000);
            Console.WriteLine("Subsequence Time: " + time / 1000 + ":" + time % 1000);
            Console.WriteLine("Subsequence1 Time: " + time1 / 1000 + ":" + time1 % 1000);
            Console.WriteLine("Subsequence2 Time: " + time2 / 1000 + ":" + time2 % 1000);

            Console.WriteLine("Wrong: " + wrong_results.Count + "/" + test_count);

            Console.WriteLine();

            if (wrong_results.Count > 0)
                Console.WriteLine("Wrong Results");
            for (int i = 0; i < wrong_results.Count; i++)
                Console.WriteLine("A: " + wrong_results[i].a + "  B: " + wrong_results[i].b + "  Subsequence Recursive: " + wrong_results[i].resultRecursive + "  Subsequence: " + wrong_results[i].result + "  Subsequence1: " + wrong_results[i].result1 + "  Subsequence2: " + wrong_results[i].result2);
        }

        private static string SubsecuenceRecursive(string a, string b)
        {
            return SubsecuenceRecursive(a, b, 0, 0) ? "YES" : "NO";
        }
        private static bool SubsecuenceRecursive(string a, string b, int x, int y)
        {
            if (x == a.Length)
                return true;
            if (y == b.Length)
                return false;

            bool result = false;
            if (a[x] == b[y])
                result = SubsecuenceRecursive(a, b, x + 1, y + 1);

            return result || SubsecuenceRecursive(a, b, x, y + 1);
        }

        private static string Subsecuence(string a, string b)
        {
            bool[,] m = new bool[a.Length, b.Length];

            for (int i = m.GetLength(1) - 1; i >= 0; i--)
                m[a.Length - 1, i] = a[a.Length - 1] == b[i] || (i < m.GetLength(1) - 1 ? m[a.Length - 1, i + 1] : false);

            for (int i = m.GetLength(0) - 2; i >= 0; i--)
                for (int j = m.GetLength(1) - 2; j >= 0; j--)
                    m[i, j] = m[i, j + 1] || (b[j] == a[i] && m[i + 1, j + 1]);

            return m[0, 0] ? "YES" : "NO";
        }

        private static string Subsecuence1(string a, string b)
        {
            bool[] current = new bool[b.Length];
            bool[] previous = new bool[b.Length];

            if (a[a.Length - 1] == b[b.Length - 1])
                previous[previous.Length - 1] = true;

            for (int i = previous.Length - 2; i >= 0; i--)
                previous[i] = a[a.Length - 1] == b[i] || previous[i + 1];

            for (int i = a.Length - 2; i >= 0; i--)
            {
                current[current.Length - (a.Length - i) + 1] = false;

                for (int j = current.Length - (a.Length - i); j >= i; j--)
                    current[j] = current[j + 1] || (b[j] == a[i] && previous[j + 1]);

                bool[] aux = previous;
                previous = current;
                current = aux;
            }

            return previous[0] ? "YES" : "NO";
        }

        private static string Subsecuence2(string a, string b)
        {
            if (b.Length < a.Length)
                return "NO";

            int lenght = b.Length - a.Length + 2;

            bool[] previous = new bool[lenght];

            bool aux = false;

            for (int i = previous.Length - 2; i >= 0; i--)
            {
                previous[i] = a[a.Length - 1] == b[a.Length - 1 + i] || previous[i + 1];
                aux = aux || previous[i];
            }

            if (!aux)
                return "NO";

            for (int i = a.Length - 2; i >= 0; i--)
            {
                bool[] current = new bool[lenght];

                aux = false;

                for (int j = current.Length - 2; j >= 0; j--)
                {
                    current[j] = current[j + 1] || (b[i + j] == a[i] && previous[j]);
                    aux = aux || current[j];
                }

                if (!aux)
                    return "NO";

                bool[] x = previous;
                previous = current;
                current = x;
            }

            return previous[0] ? "YES" : "NO";
        }
    }

    class Test
    {
        public string a { get; set; }
        public string b { get; set; }
        public string result { get; set; }
        public string result1 { get; set; }
        public string result2 { get; set; }
        public string resultRecursive { get; set; }


        public Test(string a, string b, string result, string result1, string result2, string resultRecursive)
        {
            this.a = a;
            this.b = b;
            this.result = result;
            this.result1 = result1;
            this.result2 = result2;
            this.resultRecursive = resultRecursive;
        }
    }
}
