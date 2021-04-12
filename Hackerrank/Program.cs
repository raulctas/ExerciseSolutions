using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackerrank
{
    class Solution
    {
        static void Main(String[] args)
        {
            int q = int.Parse(Console.ReadLine());

            for (int t = 0; t < q; t++)
            {
                var sv = Console.ReadLine().Trim().Split(' ').Select(x => int.Parse(x)).ToArray();

                Graph graph = new Graph(sv[0]);

                int m = sv[1];
                for (int i = 0; i < m; i++)
                {
                    var uv = Console.ReadLine().Trim().Split(' ').Select(x => int.Parse(x)).ToArray();
                    graph.AddEdge(uv[0], uv[1]);
                }

                int startId = int.Parse(Console.ReadLine().Trim());

                int[] result = graph.ShortestReach(startId);

                for (int i = 1; i < result.Length; i++)
                    if (i != startId)
                        Console.Write(result[i] + (i != result.Length - 1 ? " " : string.Empty));
                Console.WriteLine();
            }
        }
    }

    public class Graph
    {
        const int DISCONNECTED_LENGTH_EDGE = -1;
        const int LENGTH_EDGE = 6;
        List<List<int>> AdjLst;
        int Size;

        public Graph(int size)
        {
            AdjLst = new List<List<int>>();
            Size = size;
            for (int i = 0; i <= Size; i++)
                AdjLst.Add(new List<int>());
        }

        public void AddEdge(int first, int second)
        {
            AdjLst[first].Add(second);
            AdjLst[second].Add(first);
        }

        public int[] ShortestReach(int startId)
        {
            int[] distances = new int[Size + 1];
            for (int i = 0; i < distances.Length; i++)
                distances[i] = DISCONNECTED_LENGTH_EDGE;

            Queue<int> que = new Queue<int>();

            que.Enqueue(startId);
            distances[startId] = 0;

            bool[] seen = new bool[Size + 1];
            seen[startId] = true;

            while (que.Count > 0)
            {
                int curr = que.Dequeue();
                foreach (int node in AdjLst[curr])
                {
                    if (!seen[node])
                    {
                        que.Enqueue(node);
                        seen[node] = true;
                        distances[node] = distances[curr] + LENGTH_EDGE;
                    }
                }
            }
            return distances;
        }
    }
}
