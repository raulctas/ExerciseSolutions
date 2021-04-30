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
        List<StringSearchResult> ahoCorasickMatching = tree.FindAll(d);

        Dictionary<string, List<int>> ahoCorasickMatchingDict = ahoCorasickMatching.GroupBy(a => a.Keyword).ToDictionary(a => a.Key, a => a.Select(b => b.Index).ToList());
        int result = 0;
        for (int i = first; i <= last; i++)
            if (ahoCorasickMatchingDict.ContainsKey(genes[i]))
                foreach (int index in ahoCorasickMatchingDict[genes[i]])
                    if (index + genes[i].Length < last)
                        result += health[index];
        return result;
    }

    class TreeChar
    {
        public TreeCharNode Root;

        public TreeChar(List<string> words)
        {
            BuildTree(words);
        }

        private void BuildTree(List<string> words)
        {
            Root = new TreeCharNode(' ');
            foreach (string w in words)
            {
                TreeCharNode nd = Root;
                foreach (char c in w)
                {
                    TreeCharNode ndNew = null;
                    if (nd.Children.ContainsKey(c))
                        ndNew = nd.Children[c];

                    if (ndNew == null)
                    {
                        ndNew = new TreeCharNode(c, nd);
                        nd.Children.Add(c, ndNew);
                    }
                    nd = ndNew;
                }
                nd.Words.Add(w);
            }

            // Find failure functions
            Queue<TreeCharNode> nodes = new Queue<TreeCharNode>();

            // level 1 nodes - fail to root node
            foreach (TreeCharNode nd in Root.Children.Values)
            {
                nd.Failure = Root;
                foreach (TreeCharNode trans in nd.Children.Values)
                    nodes.Enqueue(trans);
            }

            // other nodes - using BFS
            while (nodes.Any())
            {
                TreeCharNode nd = nodes.Dequeue();

                TreeCharNode r = nd.Parent.Failure;

                while (r != null && !r.Children.ContainsKey(nd.Id))
                    r = r.Failure;

                if (r == null)
                    nd.Failure = Root;
                else
                {
                    nd.Failure = r.Children[nd.Id];
                    foreach (string word in nd.Failure.Words)
                        nd.Words.Add(word);
                }

                foreach (TreeCharNode child in nd.Children.Values)
                    nodes.Enqueue(child);
            }
            Root.Failure = Root;
        }

        /// <summary>
		/// Searches passed text and returns all occurrences of any keyword
		/// </summary>
		/// <param name="text">Text to search</param>
		/// <returns>Array of occurrences</returns>
		public List<StringSearchResult> FindAll(string text)
        {
            var ret = new List<StringSearchResult>();
            TreeCharNode ptr = Root;
            int index = 0;

            while (index < text.Length)
            {
                TreeCharNode trans = null;
                while (trans == null)
                {
                    trans = ptr.Children.ContainsKey(text[index]) ? ptr.Children[text[index]] : null;
                    if (ptr == Root)
                        break;
                    if (trans == null)
                        ptr = ptr.Failure;
                }
                if (trans != null)
                    ptr = trans;

                foreach (string found in ptr.Words)
                    ret.Add(new StringSearchResult(index - found.Length + 1, found));
                index++;
            }
            return ret;
        }


        /// <summary>
        /// Searches passed text and returns first occurrence of any keyword
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <returns>First occurrence of any keyword (or StringSearchResult.Empty if text doesn't contain any keyword)</returns>
        //public StringSearchResult FindFirst(string text)
        //{
        //    TreeCharNode ptr = Root;
        //    int index = 0;

        //    while (index < text.Length)
        //    {
        //        TreeCharNode trans = null;
        //        while (trans == null)
        //        {
        //            trans = ptr.Children.ContainsKey(text[index]) ? ptr.Children[text[index]] : null;
        //            if (ptr == Root)
        //                break;
        //            if (trans == null)
        //                ptr = ptr.Failure;
        //        }
        //        if (trans != null)
        //            ptr = trans;

        //        foreach (string found in ptr.Words)
        //            return new StringSearchResult(index - found.Length + 1, found);
        //        index++;
        //    }
        //    return new StringSearchResult(-1, "");
        //}


        /// <summary>
        /// Searches passed text and returns true if text contains any keyword
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <returns>True when text contains any keyword</returns>
        public bool ContainsAny(string text)
        {
            TreeCharNode ptr = Root;
            int index = 0;

            while (index < text.Length)
            {
                TreeCharNode trans = null;
                while (trans == null)
                {
                    trans = ptr.Children.ContainsKey(text[index]) ? ptr.Children[text[index]] : null;
                    if (ptr == Root)
                        break;
                    if (trans == null)
                        ptr = ptr.Failure;
                }
                if (trans != null) ptr = trans;

                if (ptr.Words.Any())
                    return true;
                index++;
            }
            return false;
        }
    }

    class TreeCharNode
    {
        public char Id;
        public TreeCharNode Parent;
        public Dictionary<char, TreeCharNode> Children;
        public List<string> Words;
        public TreeCharNode Failure;

        public TreeCharNode(char id)
        {
            Id = id;
            Children = new Dictionary<char, TreeCharNode>();
            Words = new List<string>();
        }

        public TreeCharNode(char id, TreeCharNode parent) : this(id)
        {
            Parent = parent;
        }
    }

    class StringSearchResult
    {
        public int Index;
        public string Keyword;

        public StringSearchResult(int index, string keyword)
        {
            Index = index;
            Keyword = keyword;
        }
    }
}
