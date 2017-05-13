using System;
using System.Numerics;

namespace LucSequence
{
    public struct LegendreNumbers
    {
        public int Dp { get; private set; }
        public int Dq { get; private set; }

        public long Sn { get; private set; }

        public BigInteger D { get; private set; }




        private static int Legendre(BigInteger a, BigInteger p)
        {
            if (p < 2)
                throw new ArgumentOutOfRangeException("p", "p must not be < 2");
            if (a == 0)
            {
                return 0;
            }
            if (a == 1)
            {
                return 1;
            }
            int result;
            if (a % 2 == 0)
            {
                result = Legendre(a / 2, p);
                if (((p * p - 1) & 8) != 0)
                {
                    result = -result;
                }
            }
            else
            {
                result = Legendre(p % a, a);
                if (((a - 1) * (p - 1) & 4) != 0)
                {
                    result = -result;
                }
            }
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">Message</param>
        /// <param name="q">one of the prime numbers</param>
        /// <param name="p">one of the prime numbers</param>
        public LegendreNumbers(BigInteger message, int p, int q)
        {
            if (p * q <= message) throw new Exception();

            D = (message * message) - 4;

            Dq = Legendre(D, q);
            Dp = Legendre(D, p);
            Sn = MathNet.Numerics.Euclid.LeastCommonMultiple((int)(p - Dp), (int)(q - Dq));
        }


        /// <summary>
        /// calculate nambers of Legand
        /// </summary>
        /// <param name="primeNumbers">Structure contains two prime numbers</param>
        /// <param name="message">open text  which need to encrypt</param>
        public LegendreNumbers(LucPrime primeNumbers, BigInteger message) : this(message: message, q: primeNumbers.Q, p: primeNumbers.P)
        {

        }

    }
}