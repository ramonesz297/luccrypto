using System;
using System.Numerics;

namespace LucasSequences
{
    public struct LucPublicKey : ILucKey
    {
        public BigInteger e { get; private set; }
        public BigInteger N { get; private set; }

        private const int defaultSize = 4;

        public int Size { get; private set; }

        public int MinKeyValue => (int)Math.Floor(Math.Pow(10, Size - 1));

        public int MaxKeyValue => (int)Math.Floor(Math.Pow(10, Size));

        private LucPublicKey(int size)
        {
            e = 0;
            N = 0;
            Size = size;
        }


        public LucPublicKey(BigInteger e, BigInteger n) : this((int) defaultSize)
        {
            this.e = e;
            this.N = n;
            Size = (int)Math.Floor(BigInteger.Log10(e) + 1);
        }

        public LucPublicKey(LucPrime lucPrimeNumbers) : this((int) defaultSize)
        {
            N = lucPrimeNumbers.N;
            e = GetGCDPublicKey(lucPrimeNumbers);
        }

        public LucPublicKey(LucPrime lucPrimeNumbers, int size) : this(size)
        {
            N = lucPrimeNumbers.N;
            e = GetGCDPublicKey(lucPrimeNumbers);
        }

        private BigInteger GetGCDPublicKey(LucPrime lucPrimeNumbers)
        {
            Random r = new Random();
            BigInteger e = 0;
            BigInteger t = BigInteger.Multiply(lucPrimeNumbers.P - 1, lucPrimeNumbers.Q - 1) * BigInteger.Multiply(lucPrimeNumbers.P + 1, lucPrimeNumbers.Q + 1);

            do
            {
                e = r.Next(MinKeyValue, MaxKeyValue);
            } while (BigInteger.GreatestCommonDivisor(e, t) != 1);

            return e;
        }

    }
}