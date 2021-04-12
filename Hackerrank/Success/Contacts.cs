using System;
using System.Collections.Generic;

namespace Contacts
{
    class Solution
    {
        static int[] Contacts(string[][] queries)
        {
            //List<string> contacts = new List<string>();
            Tree contacts = new Tree(new char());
            List<int> result = new List<int>();
            for (int i = 0; i < queries.Length; i++)
            {
                if (queries[i][0] == "add")
                {
                    //contacts.Add(queries[i][1]);
                    contacts.AddString(queries[i][1], 0);
                }
                else if (queries[i][0] == "find")
                {
                    //int count = 0;
                    //for (int j = 0; j < contacts.Count; j++)
                    //{
                    //    if (contacts[j].StartsWith(queries[i][1]))
                    //        count++;
                    //}
                    //result.Add(count);
                    result.Add(contacts.Find(queries[i][1], 0));
                }
            }
            return result.ToArray();
        }

        static void MainContacts(string[] args)
        {
            int queriesRows = Convert.ToInt32(Console.ReadLine());

            string[][] queries = new string[queriesRows][];

            for (int queriesRowItr = 0; queriesRowItr < queriesRows; queriesRowItr++)
                queries[queriesRowItr] = Console.ReadLine().Split(' ');

            int[] result = Contacts(queries);

            Console.WriteLine(string.Join("\n", result));
        }
    }

    class Tree
    {
        public char Id;
        public Dictionary<char, Tree> Children;
        public int NumberOfWords;

        public Tree(char id)
        {
            Id = id;
            Children = new Dictionary<char, Tree>();
        }

        public void AddString(string word, int index)
        {
            NumberOfWords++;
            if (index == word.Length)
                return;
            if (!Children.ContainsKey(word[index]))
                Children.Add(word[index], new Tree(word[index]));
            Children[word[index]].AddString(word, index + 1);
        }

        public int Find(string word, int index)
        {
            if (index == word.Length)
                return this.NumberOfWords;

            if (!Children.ContainsKey(word[index]))
                return 0;
            else
                return Children[word[index]].Find(word, index + 1);
        }
    }
}
