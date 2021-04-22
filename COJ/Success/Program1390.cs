using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ
{
    class Program1390
    {
        static void Main1390(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var numbers = new List<int>();
            while (n > 0)
            {
                int number = int.Parse(Console.ReadLine());
                numbers.Add(number);
                n--;
            }
            int maxNumber = int.MinValue;
            for (int i = 0; i < numbers.Count; i++)
                if (numbers[i] > maxNumber)
                    maxNumber = numbers[i];
            var primes = GetPrimesCriba(maxNumber);
            int maxFactor = int.MinValue;
            int maxFactorNumber = int.MinValue;
            for (int i = 0; i < numbers.Count; i++)
            {
                int currentMaxFactor = GetMaxFactorPrime(primes, numbers[i]);
                if (currentMaxFactor > maxFactor)
                {
                    maxFactor = currentMaxFactor;
                    maxFactorNumber = numbers[i];
                }
            }
            Console.WriteLine(maxFactorNumber);
        }

        private static int GetMaxFactorPrime(Dictionary<int, bool> primes, int number)
        {
            int maxFactor = int.MinValue;
            for (int i = 2; i < Math.Sqrt(number); i++)
            {
                if (number % i != 0)
                    continue;
                int factorA = i;
                int factorB = number / i;
                if (primes.ContainsKey(factorA) && factorA > maxFactor)
                    maxFactor = factorA;
                if (primes.ContainsKey(factorB) && factorB > maxFactor)
                    maxFactor = factorB;
            }
            return maxFactor;
        }

        private static Dictionary<int, bool> GetPrimesCriba(int n)
        {
            var criba = GetCriba(n);
            var primes = new Dictionary<int, bool>();
            for (int i = 2; i < criba.Length; i++)
                if (!criba[i])
                    primes.Add(i, true);
            return primes;
        }

        private static bool[] GetCriba(int n)
        {
            var criba = new bool[n];
            for (int i = 2; i < n; i++)
            {
                if (criba[i])
                    continue;
                for (int j = 2 * i; j < n; j += i)
                {
                    criba[j] = true;
                    if (j == 589)
                        ;
                }
            }
            return criba;
        }
    }
}
