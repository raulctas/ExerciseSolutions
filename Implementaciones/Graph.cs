using System.Collections.Generic;

namespace Implementaciones
{
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
