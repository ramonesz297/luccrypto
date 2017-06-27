using System;
using System.Linq;

namespace LucasSequence
{
    /// <summary>
    /// Find all prime numbers
    /// </summary>
    public class AAtkin
    {
        internal readonly int _limit;
        public bool[] IsPrimes;

        private Random random;

        public int RandomPrime
        {
            get
            {

                var index = random.Next(PrimeCount - 1);
                for (int i = 0, ip = 0; i < IsPrimes.Length; i++)
                {
                    if (!IsPrimes[i]) continue;
                    else
                    {
                        if (++ip != index) continue;
                        else
                        {
                            IsPrimes[i] = false;
                            return i;
                        }
                    }
                }
                return -1;
            }
        }
        public int PrimeCount => IsPrimes.Count(x => x);


        public AAtkin(int limit, Random random)
        {
            this.random = random;
            _limit = limit;
            IsPrimes = new bool[_limit + 1];
            FindPrimes();
        }

        public AAtkin(int limit)
        {
            _limit = limit;
            IsPrimes = new bool[_limit + 1];
            FindPrimes();
            random = new Random();
        }

        public void FindPrimes()
        {
            double sqrt = Math.Sqrt(_limit);
            var limit = (ulong)_limit;
            for (ulong x = 1; x <= sqrt; x++)
                for (ulong y = 1; y <= sqrt; y++)
                {
                    ulong x2 = x * x;
                    ulong y2 = y * y;
                    ulong n = 4 * x2 + y2;
                    if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                        IsPrimes[n] ^= true;

                    n -= x2;
                    if (n <= limit && n % 12 == 7)
                        IsPrimes[n] ^= true;

                    n -= 2 * y2;
                    if (x > y && n <= limit && n % 12 == 11)
                        IsPrimes[n] ^= true;
                }

            for (ulong n = 5; n <= sqrt; n += 2)
                if (IsPrimes[n])
                {
                    ulong s = n * n;
                    for (ulong k = s; k <= limit; k += s)
                        IsPrimes[k] = false;
                }
            IsPrimes[2] = true;
            IsPrimes[3] = true;
        }
    }
}
