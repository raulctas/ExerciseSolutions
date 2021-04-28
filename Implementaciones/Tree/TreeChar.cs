using System.Collections.Generic;

namespace Implementaciones
{
    class TreeChar
    {
        public TreeCharNode Root;

        public TreeChar(List<string> words)
        {
            Root = new TreeCharNode(' ');
            foreach (var word in words)
                AddString(Root, word, 0);
        }

        public void AddString(string word)
        {
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
                node.Children.Add(word[index], new TreeCharNode(word[index]));
            AddString(node.Children[word[index]], word, index + 1);
        }

        public int Find(string word)
        {
            return Find(Root, word, 0);
        }

        private int Find(TreeCharNode node, string word, int index)
        {
            if (index == word.Length)
                return node.NumberOfWords;

            if (!node.Children.ContainsKey(word[index]))
                return 0;
            else
                return Find(node.Children[word[index]], word, index + 1);
        }

        public Dictionary<string, List<int>> AhoCorasickMatching(string word)
        {
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();

            TreeCharNode current = Root;

            for (int i = 0; i < word.Length; i++)
            {
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
                else
                    current = Root;
            }

            return result;
        }
    }

    class TreeCharNode
    {
        public char Id;
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
