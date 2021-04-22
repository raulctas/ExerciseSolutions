using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COJ
{
    class Primes
    {
        static Random r = new Random();

        static void MainPrimes(string[] args)
        {
            //int n = 100000000;
            int n = 1000000;
            var primesCribaEratostenes = GetPrimesCribaEratostenes(n);
            int k = 100;
            var primesMiller = GetPrimesMillerTest(n, k);
            int iteraciones = 10;
            var primesSolovoyStrassen = GetPrimesSolovoyStrassen(n, iteraciones);

            var maxCount = Math.Max(primesCribaEratostenes.Count, Math.Max(primesMiller.Count, primesSolovoyStrassen.Count));
            for (int i = 0; i < maxCount; i++)
            {
                var primeCribaEratostenes = i < primesCribaEratostenes.Count ? primesCribaEratostenes[i].ToString() : string.Empty;
                var primeMiller = i < primesMiller.Count ? primesMiller[i].ToString() : string.Empty;
                var primeSolovoyStrassen = i < primesSolovoyStrassen.Count ? primesSolovoyStrassen[i].ToString() : string.Empty;

                if (primeCribaEratostenes != primeMiller || primeCribaEratostenes != primeSolovoyStrassen)
                    Console.WriteLine($"Eratostenes: {primeCribaEratostenes} Miler: {primeMiller} SolovoyStrassen: {primeSolovoyStrassen}");
            }
            Console.WriteLine("Final");
        }

        //Miller-Rabin
        private static List<int> GetPrimesMillerTest(int n, int k)
        {
            var primes = new List<int>();

            for (int i = 2; i < n; i++)
                if (IsPrime(i, k))
                    primes.Add(i);

            return primes;
        }
        // It returns false if n is composite 
        // and returns true if n is probably 
        // prime. k is an input parameter that 
        // determines accuracy level. Higher 
        // value of k indicates more accuracy. 
        static bool IsPrime(int n, int k)
        {
            Random r = new Random();

            // Corner cases 
            if (n <= 1 || n == 4)
                return false;
            if (n <= 3)
                return true;

            // Find r such that n = 2^d * r + 1 
            // for some r >= 1 
            int d = n - 1;

            while (d % 2 == 0)
                d /= 2;

            // Iterate given nber of 'k' times 
            for (int i = 0; i < k; i++)
                if (MillerTest(d, n, r) == false)
                    return false;

            return true;
        }
        // Utility function to do modular 
        // exponentiation. It returns (x^y) % p 
        static int Power(int x, int y, int p)
        {
            int res = 1; // Initialize result 

            // Update x if it is more than 
            // or equal to p 
            x = x % p;

            while (y > 0)
            {

                // If y is odd, multiply x with result 
                if ((y & 1) == 1)
                    res = (res * x) % p;

                // y must be even now 
                y = y >> 1; // y = y/2 
                x = (x * x) % p;
            }

            return res;
        }
        // This function is called for all k trials. 
        // It returns false if n is composite and 
        // returns false if n is probably prime. 
        // d is an odd number such that d*2<sup>r</sup> 
        // = n-1 for some r >= 1 
        static bool MillerTest(int d, int n, Random r)
        {

            // Pick a random number in [2..n-2] 
            // Corner cases make sure that n > 4 

            int a = 2 + (int)(r.Next() % (n - 4));

            // Compute a^d % n 
            int x = Power(a, d, n);

            if (x == 1 || x == n - 1)
                return true;

            // Keep squaring x while one of the 
            // following doesn't happen 
            // (i) d does not reach n-1 
            // (ii) (x^2) % n is not 1 
            // (iii) (x^2) % n is not n-1 
            while (d != n - 1)
            {
                x = (x * x) % n;
                d *= 2;

                if (x == 1)
                    return false;
                if (x == n - 1)
                    return true;
            }

            // Return composite 
            return false;
        }

        //Criba de Eratóstenes
        private static List<int> GetPrimesCribaEratostenes(int n)
        {
            var criba = GetCribaEratostenes(n);
            var primes = new List<int>();
            for (int i = 2; i < criba.Length; i++)
                if (!criba[i])
                    primes.Add(i);
            return primes;
        }
        private static bool[] GetCribaEratostenes(int n)
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
