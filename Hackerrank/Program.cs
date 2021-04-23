using System;
using System.Collections.Generic;

class Solution
{
    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] genes = Console.ReadLine().Split(' ');
        int[] health = Array.ConvertAll(Console.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
        int s = Convert.ToInt32(Console.ReadLine());

        Dictionary<int, Dictionary<int, Dictionary<string, int>>> data = BuildData(genes, health);

        int unhealthiest = int.MaxValue;
        int healthiest = int.MinValue;
        for (int sItr = 0; sItr < s; sItr++)
        {
            string[] firstLastd = Console.ReadLine().Split(' ');
            int first = Convert.ToInt32(firstLastd[0]);
            int last = Convert.ToInt32(firstLastd[1]);
            string d = firstLastd[2];

            int value = GetTotalHealth(data, first, last, d);
            if (value < unhealthiest)
                unhealthiest = value;
            if (value > healthiest)
                healthiest = value;
        }
        Console.WriteLine($"{unhealthiest} {healthiest}");
    }

    private static Dictionary<int, Dictionary<int, Dictionary<string, int>>> BuildData(string[] genes, int[] health)
    {
        Dictionary<int, Dictionary<int, Dictionary<string, int>>> data = new Dictionary<int, Dictionary<int, Dictionary<string, int>>>();
        for (int i = 0; i < genes.Length; i++)
        {
            if (!data.ContainsKey(i))
                data[i] = new Dictionary<int, Dictionary<string, int>>();
            for (int j = i; j < genes.Length; j++)
            {
                if (!data[i].ContainsKey(j))
                    data[i][j] = j == i ? new Dictionary<string, int>() : new Dictionary<string, int>(data[i][j - 1]);
                if (data[i][j].ContainsKey(genes[j]))
                    data[i][j][genes[j]] += health[j];
                else
                    data[i][j][genes[j]] = health[j];
            }
        }
        return data;
    }

    private static int GetTotalHealth(Dictionary<int, Dictionary<int, Dictionary<string, int>>> data, int first, int second, string d)
    {
        int result = 0;
        for (int i = 0; i < d.Length; i++)
        {
            bool subsequentFound = false;
            for (int j = i; j < d.Length; j++)
            {
                string s = d.Substring(i, j - i + 1);
                if (data[first][second].ContainsKey(s))
                {
                    result += data[first][second][s];
                    subsequentFound = true;
                }
                else if (subsequentFound)
                    break;
            }
        }
        return result;
    }
}
