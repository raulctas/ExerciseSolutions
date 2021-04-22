using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackerrank
{
    class SwapNodes1
    {
        static int[][] SwapNodes(int[][] indexes, int[] queries)
        {
            Queue<Node> q = new Queue<Node>();

            Node root = new Node(1, 1);

            for (int i = 0; i < indexes.Length; i++)
            {
                Node current = q.Count == 0 ? root : q.Dequeue();
                int left = indexes[i][0];
                int right = indexes[i][1];
                if (left != -1)
                {
                    current.LeftNode = new Node(left, current.Level + 1);
                    q.Enqueue(current.LeftNode);
                }
                if (right != -1)
                {
                    current.RightNode = new Node(right, current.Level + 1);
                    q.Enqueue(current.RightNode);
                }
            }

            int[][] result = new int[queries.Length][];
            for (int i = 0; i < queries.Length; i++)
            {
                int k = queries[i];
                root.SwapNodes(k);
                result[i] = root.InOrder().ToArray();
            }
            return result;
        }

        static void MainSwapNodes(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            int[][] indexes = new int[n][];

            for (int indexesRowItr = 0; indexesRowItr < n; indexesRowItr++)
            {
                indexes[indexesRowItr] = Array.ConvertAll(Console.ReadLine().Split(' '), indexesTemp => Convert.ToInt32(indexesTemp));
            }

            int queriesCount = Convert.ToInt32(Console.ReadLine());

            int[] queries = new int[queriesCount];

            for (int queriesItr = 0; queriesItr < queriesCount; queriesItr++)
            {
                int queriesItem = Convert.ToInt32(Console.ReadLine());
                queries[queriesItr] = queriesItem;
            }

            int[][] result = SwapNodes(indexes, queries);

            Console.WriteLine(String.Join("\n", result.Select(x => String.Join(" ", x))));
        }

        class Node
        {
            public Node LeftNode { get; set; }
            public Node RightNode { get; set; }
            public int Id { get; set; }
            public int Level { get; set; }

            public Node(int id, int level)
            {
                Id = id;
                Level = level;
            }

            public void SwapNodes(int k)
            {
                if (Level % k == 0)
                {
                    Node aux = RightNode;
                    RightNode = LeftNode;
                    LeftNode = aux;
                }
                if (LeftNode != null)
                    LeftNode.SwapNodes(k);
                if (RightNode != null)
                    RightNode.SwapNodes(k);
            }

            public List<int> InOrder()
            {
                List<int> result = new List<int>();
                if (LeftNode != null)
                    result.AddRange(LeftNode.InOrder());
                result.Add(Id);
                if (RightNode != null)
                    result.AddRange(RightNode.InOrder());
                return result;
            }
        }
    }
}
