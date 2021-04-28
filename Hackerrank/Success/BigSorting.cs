using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Hackerrank
{
    class BigSorting
    {

        // Complete the bigSorting function below.
        static string[] bigSorting(string[] unsorted)
        {
            List<StringNumber> unsortedList = unsorted.ToList().Select(s => new StringNumber(s)).ToList();
            unsortedList.Sort();
            return unsortedList.Select(s => s.Number).ToArray();
        }

        static void MainBigSorting(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int n = Convert.ToInt32(Console.ReadLine());

            string[] unsorted = new string[n];

            for (int i = 0; i < n; i++)
            {
                string unsortedItem = Console.ReadLine();
                unsorted[i] = unsortedItem;
            }

            string[] result = bigSorting(unsorted);

            textWriter.WriteLine(string.Join("\n", result));

            textWriter.Flush();
            textWriter.Close();
        }
    }

    public class StringNumber : IComparable
    {
        public string Number;

        public StringNumber(string number)
        {
            Number = number;
        }

        public int CompareTo(object obj)
        {
            StringNumber objStringNumber = obj as StringNumber;

            if (objStringNumber == null)
                return 1;

            if (Number.Length > objStringNumber.Number.Length)
                return 1;
            else if (Number.Length < objStringNumber.Number.Length)
                return -1;

            for (int i = 0; i < Number.Length; i++)
            {
                char n = Number[i];
                char nObj = objStringNumber.Number[i];

                if (n < nObj)
                    return -1;
                else if (n > nObj)
                    return 1;
            }

            return 0;
        }
    }
}
