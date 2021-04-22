using System;

namespace Implementaciones
{
    class OperacionesExpresiones
    {
        static void MainOperacionesExpresiones(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Operación:");
                string s = Console.ReadLine();

                Node result = CheckExp(s, 0, s.Length - 1);
                Console.WriteLine("Is OK: " + result.isOK + (result.isOK ? " Value:" + result.value : ""));
            }
        }

        private static Node CheckExp(string s, int start, int end)
        {
            if (start > end)
                return new Node();

            int mo = MO(s, ref start, ref end);

            if (mo != -1)
            {
                Node left = CheckExp(s, start, mo - 1);
                Node right = CheckExp(s, mo + 1, end);

                return new Node(s[mo], CalcValue(left.value, right.value, s[mo]), left, right, left.isOK && right.isOK && ((s[mo] == '/' && right.value != 0) || s[mo] != '/'));
            }
            else
                return IsNumber(s, start, end);
        }

        private static double CalcValue(double p1, double p2, char oper)
        {
            switch (oper)
            {
                case '+':
                    return p1 + p2;
                case '-':
                    return p1 - p2;
                case '*':
                    return p1 * p2;
                case '/':
                    if (p2 != 0)
                        return p1 / p2;
                    else
                        return 0;
                default:
                    return 0;
            }
        }

        private static Node IsNumber(string s, int start, int end)
        {
            int signo = 0;

            for (int j = start; j <= end; j++)
                if ((s[j] == ',' || s[j] == '.') && signo == 0)
                    signo++;
                else if (!char.IsDigit(s[j]) || signo == 1)
                    return new Node();

            if (start <= end)
                return new Node(double.Parse(s.Substring(start, end - start + 1)), true);
            else
                return new Node();
        }

        private static int MO(string s, ref int i, ref int f)
        {
            int count_parent = 0;

            if (i == f)
                return -1;

            for (int j = i; j < f; j++)
            {
                if (s[j] == '(')
                    count_parent++;
                else if (s[j] == ')')
                    count_parent--;

                if ((s[j + 1] == '+' || s[j + 1] == '-' || s[j + 1] == '*' || s[j + 1] == '/') && count_parent == 0)
                    return j + 1;
            }

            if (s[f] == ')')
                count_parent--;

            if (count_parent == 0 && s[i] == '(' && s[f] == ')')
            {
                i++; f--;
                return MO(s, ref i, ref f);
            }

            return -1;
        }
    }

    class Node
    {
        public char oper { get; set; }
        public double value { get; set; }
        public bool isOK { get; set; }

        public Node left { get; set; }
        public Node right { get; set; }

        public Node(char oper, double value, Node left, Node right, bool isOK)
        {
            this.oper = oper;
            this.value = double.Parse(value.ToString("F2"));
            this.left = left;
            this.right = right;
            this.isOK = isOK;
        }

        public Node() { }

        public Node(double value, bool isOK)
        {
            this.value = double.Parse(value.ToString("F2"));
            this.isOK = isOK;
        }
    }
}
