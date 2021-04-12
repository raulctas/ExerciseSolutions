using System;
using System.Collections.Generic;

namespace BalancedBrackets
{
    class Solution
    {
        static string IsBalanced(string s)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '{' || s[i] == '[' || s[i] == '(')
                    stack.Push(s[i]);
                else if (s[i] == '}')
                {
                    if (stack.Count == 0 || stack.Peek() != '{')
                        return "NO";
                    stack.Pop();
                }
                else if (s[i] == ']')
                {
                    if (stack.Count == 0 || stack.Peek() != '[')
                        return "NO";
                    stack.Pop();
                }
                else if (s[i] == ')')
                {
                    if (stack.Count == 0 || stack.Peek() != '(')
                        return "NO";
                    stack.Pop();
                }
            }

            if (stack.Count == 0)
                return "YES";
            else
                return "NO";
        }

        static void MainBalancedBrackets(string[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());

            for (int tItr = 0; tItr < t; tItr++)
            {
                string s = Console.ReadLine();

                string result = IsBalanced(s);

                Console.WriteLine(result);
            }
        }
    }
}
