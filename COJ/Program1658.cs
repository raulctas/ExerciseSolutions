using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ
{
    class Program1658
    {
        static void Main1658(string[] args)
        {
            int cases = int.Parse(Console.ReadLine());
            while (cases > 0)
            {
                int n = int.Parse(Console.ReadLine());
                var lineItem = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var numbers = new int[n];
                for (int i = 0; i < n; i++)
                    numbers[i] = int.Parse(lineItem[i]);

                var result = 1;

                int[] line = new int[n];
                line[0] = 1;

                for (int i = 1; i < n; i++)
                {
                    int max = int.MinValue;
                    for (int j = i - 1; j >= 0; j--)
                        if (numbers[i] > numbers[j] && line[j] > max)
                            max = line[j];
                    line[i] = 1 + max;
                    if (line[i] > result)
                        result = line[i];
                }

                Console.WriteLine(result);
                cases--;
            }
        }
    }
}
