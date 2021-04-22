using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ
{
    class Program2019
    {
        static Random r = new Random();

        static void Main2019(string[] args)
        {
            while (true)
            {
                string n = Console.ReadLine();
                Console.WriteLine(GetPrimality(n));
            }
        }

        private static int GetPrimality(string n)
        {
            int iteraciones = 10;
            int primatily = 0;
            var numbers = GetNumbers(n);
            foreach (var number in numbers.Keys)
                if (SolovoyStrassen(number, iteraciones))
                    primatily++;
            return primatily;
        }

        private static Dictionary<int, bool> GetNumbers(string n)
        {
            var numbers = new Dictionary<int, bool>();
            var currentNumber = new List<char>();
            var digitsUsed = new bool[n.Length];
            GetNumbers(numbers, n, currentNumber, digitsUsed);
            return numbers;
        }

        private static void GetNumbers(Dictionary<int, bool> numbers, string n, List<char> currentNumber, bool[] digitsUsed)
        {
            if (currentNumber.Count == n.Length)
            {
                var number = GetNumbers(currentNumber);
                numbers[number] = true;
                return;
            }

            for (int i = 0; i < n.Length; i++)
            {
                if (digitsUsed[i])
                    continue;
                currentNumber.Add(n[i]);
                digitsUsed[i] = true;
                GetNumbers(numbers, n, currentNumber, digitsUsed);
                currentNumber.RemoveAt(currentNumber.Count - 1);
                digitsUsed[i] = false;
            }
        }

        private static int GetNumbers(List<char> currentNumber)
        {
            string number = string.Empty;
            for (int i = 0; i < currentNumber.Count; i++)
                number += currentNumber[i];
            return int.Parse(number);
        }

        //Solovay-Strassen
        // modulo function to perform binary exponentiation 
        private static List<int> GetPrimesSolovoyStrassen(int n, int iteraciones)
        {
            var primes = new List<int>();

            for (int i = 2; i < n; i++)
                if (SolovoyStrassen(i, iteraciones))
                    primes.Add(i);

            return primes;
        }
        private static long Modulo(long baseP, long exponent, long mod)
        {
            long x = 1;
            long y = baseP;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    x = (x * y) % mod;

                y = (y * y) % mod;
                exponent = exponent / 2;
            }
            return x % mod;
        }
        // To calculate Jacobian symbol of a given number 
        private static int CalculateJacobian(long a, long n)
        {
            if (a == 0)
                return 0;// (0/n) = 0 

            int ans = 1;
            if (a < 0)
            {
                a = -a; // (a/n) = (-a/n)*(-1/n) 
                if (n % 4 == 3)
                    ans = -ans; // (-1/n) = -1 if n = 3 (mod 4) 
            }

            if (a == 1)
                return ans;// (1/n) = 1 

            while (a != 0)
            {
                if (a < 0)
                {
                    a = -a;// (a/n) = (-a/n)*(-1/n) 
                    if (n % 4 == 3)
                        ans = -ans;// (-1/n) = -1 if n = 3 (mod 4) 
                }

                while (a % 2 == 0)
                {
                    a = a / 2;
                    if (n % 8 == 3 || n % 8 == 5)
                        ans = -ans;

                }

                Swap(ref a, ref n);

                if (a % 4 == 3 && n % 4 == 3)
                    ans = -ans;
                a = a % n;

                if (a > n / 2)
                    a = a - n;
            }

            if (n == 1)
                return ans;

            return 0;
        }
        private static void Swap(ref long a, ref long n)
        {
            long b = a;
            a = n;
            n = b;
        }
        // To perform the Solovay-Strassen Primality Test 
        private static bool SolovoyStrassen(long p, int iterations)
        {
            if (p < 2)
                return false;
            if (p != 2 && p % 2 == 0)
                return false;

            for (int i = 0; i < iterations; i++)
            {
                // Generate a random number a 
                long a = r.Next() % (p - 1) + 1;
                long jacobian = (p + CalculateJacobian(a, p)) % p;
                long mod = Modulo(a, (p - 1) / 2, p);

                if (jacobian == 0 || mod != jacobian)
                    return false;
            }
            return true;
        }
    }
}
