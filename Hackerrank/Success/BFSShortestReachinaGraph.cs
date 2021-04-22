using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackerrank
{
    class BFSShortestReachinaGraph
    {
        const int LENGTH_EDGE = 6;
        const int DISCONNECTED_LENGTH_EDGE = -1;

        static void MainBFSShortestReachinaGraph(String[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your    class should be named Solution */
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

                //List<int> result = BfsShortestReachGraph(graph, startId);
                //for (int i = 0; i < result.Count; i++)
                //    Console.Write(result[i] + (i != result.Count - 1 ? " " : string.Empty));
                //Console.WriteLine();

                int[] result = GetDistance(graph, startId);
                for (int i = 0; i < result.Length; i++)
                    if (i != startId - 1)
                        Console.Write(result[i] + (i != result.Length - 1 ? " " : string.Empty));
                Console.WriteLine();
            }
        }

        static List<int> BfsShortestReachGraph(Graph graph, int sId)
        {
            Node s = graph.Nodes[sId];

            Dictionary<int, bool> visitedNode = new Dictionary<int, bool>();
            visitedNode[sId] = true;
            List<int> resultBfs = new List<int>();
            Bfs(s, ref visitedNode, 0, ref resultBfs);

            List<int> result = new List<int>();
            bool resultBfsAdded = false;
            foreach (int id in graph.Nodes.Keys)
            {
                if (!visitedNode.ContainsKey(id))
                    result.Add(DISCONNECTED_LENGTH_EDGE);
                else if (!resultBfsAdded)
                {
                    resultBfsAdded = true;
                    result.AddRange(resultBfs);
                }
            }

            return result;
        }

        static void Bfs(Node s, ref Dictionary<int, bool> visitedNode, int length, ref List<int> result)
        {
            foreach (int id in s.Nodes.Keys)
            {
                if (visitedNode.ContainsKey(id))
                    continue;
                visitedNode[id] = true;
                result.Add(length + LENGTH_EDGE);
                Bfs(s.Nodes[id], ref visitedNode, length + LENGTH_EDGE, ref result);
            }
        }

        static int[] GetDistance(Graph graph, int startId)
        {
            int[] results = new int[graph.Nodes.Count];

            Node s = graph.Nodes[startId];

            Queue<Node> q = new Queue<Node>();
            bool[] isVisited = new bool[graph.Nodes.Count];
            q.Enqueue(s);
            isVisited[s.Id - 1] = true;

            int count = 0;
            while (q.Any())
            {
                int qSize = q.Count();
                for (int i = 0; i < qSize; i++)
                {
                    Node removed = q.Dequeue();
                    results[removed.Id - 1] = count;
                    foreach (int x in removed.Nodes.Keys)
                    {
                        if (!isVisited[x - 1])
                        {
                            q.Enqueue(removed.Nodes[x]);
                            isVisited[x - 1] = true;
                        }
                    }
                }
                count += LENGTH_EDGE;
            }

            for (int i = 0; i < results.Length; i++)
                if (results[i] == 0)
                    results[i] = -1;

            return results;
        }
    }

    class Graph
    {
        public Dictionary<int, Node> Nodes;

        public Graph(int n)
        {
            Nodes = new Dictionary<int, Node>();
            for (int i = 1; i <= n; i++)
                Nodes.Add(i, new Node(i));
        }

        public void AddEdge(int u, int v)
        {
            Node nodeU = Nodes[u];
            Node nodeV = Nodes[v];
            if (!nodeU.Nodes.ContainsKey(v))
                nodeU.Nodes.Add(v, nodeV);
            if (!nodeU.Nodes.ContainsKey(u))
                nodeV.Nodes.Add(u, nodeU);
        }
    }

    class Node
    {
        public int Id;
        public SortedList<int, Node> Nodes;

        public Node(int id)
        {
            Id = id;
            Nodes = new SortedList<int, Node>();
        }
    }
}
