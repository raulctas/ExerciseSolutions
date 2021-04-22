using System;
using System.Collections.Generic;

namespace Hackerrank.Implementations
{
    class BinaryTree
    {
        BinaryTreeNode Root;

        public void SwapNodes(BinaryTreeNode node, int k)
        {
            if (node.Level % k == 0)
            {
                BinaryTreeNode aux = node.RightNode;
                node.RightNode = node.LeftNode;
                node.LeftNode = aux;
            }
            if (node.LeftNode != null)
                SwapNodes(node.LeftNode, k);
            if (node.RightNode != null)
                SwapNodes(node.RightNode, k);
        }

        public List<int> InOrder(BinaryTreeNode node)
        {
            List<int> result = new List<int>();
            if (node.LeftNode != null)
                result.AddRange(InOrder(node.LeftNode));
            result.Add(node.Data);
            if (node.RightNode != null)
                result.AddRange(InOrder(node.RightNode));
            return result;
        }

        public List<int> LevelOrder(BinaryTreeNode root)
        {
            List<int> result = new List<int>();

            Queue<BinaryTreeNode> q = new Queue<BinaryTreeNode>();
            q.Enqueue(root);
            result.Add(root.Data);

            while (q.Count != 0)
            {
                BinaryTreeNode currentNode = q.Dequeue();
                if (currentNode.LeftNode != null)
                {
                    result.Add(currentNode.LeftNode.Data);
                    q.Enqueue(currentNode.LeftNode);
                }
                if (currentNode.RightNode != null)
                {
                    result.Add(currentNode.RightNode.Data);
                    q.Enqueue(currentNode.RightNode);
                }
            }

            return result;
        }

        public int Height(BinaryTreeNode node)
        {
            int leftHeight = 0;
            if (node.LeftNode != null)
                leftHeight = 1 + Height(node.LeftNode);

            int rightHeight = 0;
            if (node.RightNode != null)
                rightHeight = 1 + Height(node.RightNode);

            return Math.Max(leftHeight, rightHeight);
        }

        public BinaryTreeNode LowestCommonAncestor(BinaryTreeNode node, int a, int b)
        {
            if (a < node.Data && b < node.Data)
                return LowestCommonAncestor(node.LeftNode, a, b);
            if (a > node.Data && b > node.Data)
                return LowestCommonAncestor(node.RightNode, a, b);
            return node;
        }

        public bool CheckBst(BinaryTreeNode node)
        {
            return CheckBst(node, int.MinValue, int.MaxValue);
        }

        private bool CheckBst(BinaryTreeNode node, int min, int max)
        {
            bool leftBst = CheckBst(node.LeftNode, min, Math.Min(max, node.Data));
            bool rightBst = CheckBst(node.RightNode, Math.Max(min, node.Data), max);
            return node.Data > min && node.Data < max && leftBst && rightBst;
        }

        public string HuffmanDecoding(BinaryTreeNode root, string s)
        {
            string result = string.Empty;
            BinaryTreeNode current = root;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    current = current.LeftNode;
                else if (s[i] == '1')
                    current = current.RightNode;
                if (current.IsLeaf)
                {
                    result += current.Data;
                    current = root;
                }
            }
            return result;
        }
    }

    class BinaryTreeNode
    {
        public BinaryTreeNode LeftNode { get; set; }
        public BinaryTreeNode RightNode { get; set; }
        public int Data { get; set; }
        public int Frequency { get; set; }
        public int Level { get; set; }
        public bool IsLeaf
        {
            get
            {
                return LeftNode == null && RightNode == null;
            }
        }

        public BinaryTreeNode(int id, int level)
        {
            Data = id;
            Level = level;
        }
    }
}
