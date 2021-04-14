using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackerrank
{
    class Solution
    {
        static int balancedForest(int[] c, int[][] edges)
        {
            Dictionary<int, Node> nodes = new Dictionary<int, Node>();
            for (int i = 0; i < c.Length; i++)
                nodes.Add(i + 1, new Node(i + 1, c[i]));
            for (int i = 0; i < edges.Length; i++)
            {
                int father = edges[i][0];
                int child = edges[i][1];
                nodes[father].AddNode(nodes[child]);
            }

            int total = nodes.Values.Sum(n => n.Value);

            List<int> values = nodes.Values.Select(n => n.TreeValue).ToList();
            values.Sort();

            for (int i = values.Count - 2; i > 0; i--)
                if (values[i] == values[i - 1])
                    return total - 2 * values[i];
            return -1;
        }

        static void Main(string[] args)
        {
            int q = Convert.ToInt32(Console.ReadLine());

            for (int qItr = 0; qItr < q; qItr++)
            {
                int n = Convert.ToInt32(Console.ReadLine());

                int[] c = Array.ConvertAll(Console.ReadLine().Split(' '), cTemp => Convert.ToInt32(cTemp))
                ;

                int[][] edges = new int[n - 1][];

                for (int i = 0; i < n - 1; i++)
                {
                    edges[i] = Array.ConvertAll(Console.ReadLine().Split(' '), edgesTemp => Convert.ToInt32(edgesTemp));
                }

                int result = balancedForest(c, edges);

                Console.WriteLine(result);
            }
        }
    }

    class Tree
    {
        Node Root;

        public Tree()
        {

        }
    }

    class Node
    {
        public Dictionary<int, Node> Nodes;
        public int Id { get; set; }
        public int Value { get; set; }
        public int TreeValue { get; set; }

        public Node(int id, int value)
        {
            Id = id;
            Value = value;
            TreeValue = value;
            Nodes = new Dictionary<int, Node>();
        }

        public void AddNode(Node node)
        {
            Nodes[node.Id] = node;
            TreeValue += node.TreeValue;
        }
    }
}
