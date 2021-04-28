using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] genes = Console.ReadLine().Split(' ');
        int[] health = Array.ConvertAll(Console.ReadLine().Split(' '), healthTemp => Convert.ToInt32(healthTemp));
        int s = Convert.ToInt32(Console.ReadLine());

        TreeChar root = new TreeChar(genes.ToList());

        int unhealthiest = int.MaxValue;
        int healthiest = int.MinValue;
        for (int sItr = 0; sItr < s; sItr++)
        {
            string[] firstLastd = Console.ReadLine().Split(' ');
            int first = Convert.ToInt32(firstLastd[0]);
            int last = Convert.ToInt32(firstLastd[1]);
            string d = firstLastd[2];

            int value = GetTotalHealth(root, genes, health, first, last, d);
            if (value < unhealthiest)
                unhealthiest = value;
            if (value > healthiest)
                healthiest = value;
        }
        Console.WriteLine($"{unhealthiest} {healthiest}");
    }

    private static int GetTotalHealth(TreeChar tree, string[] genes, int[] health, int first, int last, string d)
    {
        Dictionary<string, List<int>> ahoCorasickMatching = tree.AhoCorasickMatching(d);
        int result = 0;
        for (int i = first; i <= last; i++)
            if (ahoCorasickMatching.ContainsKey(genes[i]))
                ahoCorasickMatching[genes[i]].ForEach(a => result += health[i]);
        return result;
    }

    class TreeChar
    {
        public TreeCharNode Root;

        public TreeChar(List<string> words)
        {
            Root = new TreeCharNode(' ');
            foreach (var word in words)
                AddString(Root, word, 0);
        }

        private void AddString(TreeCharNode node, string word, int index)
        {
            node.NumberOfWords++;
            if (index == word.Length)
            {
                node.Word = word;
                return;
            }
            if (!node.Children.ContainsKey(word[index]))
            {
                TreeCharNode newTreeCharNode = new TreeCharNode(word[index]);
                newTreeCharNode.Parent = node;
                node.Children.Add(word[index], newTreeCharNode);
            }
            AddString(node.Children[word[index]], word, index + 1);
        }

        public Dictionary<string, List<int>> AhoCorasickMatching(string word)
        {
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();

            TreeCharNode current = Root;

            for (int i = 0; i < word.Length; i++)
            {

                TreeCharNode trans = null;
                while (trans == null)
                {
                    if (current.Children.ContainsKey(word[i]))
                        trans = current.Children[word[i]];
                    if (current == Root)
                        break;
                    if (trans == null)
                        current = current.Parent;
                }
                if (trans != null)
                    current = trans;

                if (current.Children.ContainsKey(word[i]))
                {
                    current = current.Children[word[i]];
                    if (!string.IsNullOrEmpty(current.Word))
                    {
                        if (!result.ContainsKey(current.Word))
                            result[current.Word] = new List<int>();
                        result[current.Word].Add(i - current.Word.Length + 1);
                    }
                }
            }

            return result;
        }
    }

    class TreeCharNode
    {
        public char Id;
        public TreeCharNode Parent;
        public Dictionary<char, TreeCharNode> Children;
        public int NumberOfWords;
        public string Word;

        public TreeCharNode(char id)
        {
            Id = id;
            Children = new Dictionary<char, TreeCharNode>();
        }
    }
}
