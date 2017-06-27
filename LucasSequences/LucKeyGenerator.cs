using System;
using System.Numerics;

namespace LucasSequences
{
    public class LucKeyGenerator
    {
        private readonly LucPrime PrimeNumbers;

        private readonly BigInteger P;

        private LucPublicKey _publicKey;

        public LucPublicKey PublicKey
        {
            get
            {
                return _publicKey;
            }
            private set
            {
                _publicKey = value;
            }
        }

        private LucPrivateKey _privateKey;
        public LucPrivateKey PrivateKey
        {
            get
            {
                return _privateKey;
            }
            private set
            {
                _privateKey = value;
            }
        }

        private BigInteger GetD()
        {
            return (P * P) - 4;
        }

        /// <summary>
        /// S (N) parameter
        /// </summary>
        /// <returns>return Lcm between Legendre</returns>
        private BigInteger GetLcm()
        {
            BigInteger d = GetD();
            LegendreNumbers legendresNumbers = new LegendreNumbers(d, PrimeNumbers.Q, PrimeNumbers.P);

            int a = PrimeNumbers.P - legendresNumbers.Dp;
            int b = PrimeNumbers.Q - legendresNumbers.Dq;

            return MathNet.Numerics.Euclid.LeastCommonMultiple(a, b);
        }

        private BigInteger getSmalld(LucPublicKey key)
        {

            var lcm = (long)GetLcm();


            BigInteger e  = BigInteger.ModPow(key.e, (int)(lcm.Eyler() - 1), lcm);

            return e;
        }


        /// <summary>
        /// It`s Private keys
        /// </summary>
        /// <returns></returns>
        private BigInteger GenerateSimpleE()
        {
            BigInteger t = (PrimeNumbers.P - 1) * (PrimeNumbers.Q + 1) * (PrimeNumbers.P + 1) * (PrimeNumbers.Q + 1);
            int e ;
            Random r = new Random();
            do
            {
                e = r.Next(100, PrimeNumbers.N);

            } while (e >= PrimeNumbers.N || MathNet.Numerics.Euclid.GreatestCommonDivisor(e, t) != 1);

            return e;
        }


        public LucKeyGenerator(LucPrime prime, BigInteger message)
        {
            P = message;
            PrimeNumbers = prime;
            if (PrimeNumbers.N <= message)
                throw new ArgumentException("N need to be greater than message");
            PublicKey = new LucPublicKey(GenerateSimpleE(), PrimeNumbers.N);

            PrivateKey = new LucPrivateKey(getSmalld(PublicKey), PrimeNumbers.N);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="message"></param>
        public LucKeyGenerator(int p, int q, BigInteger message)
        {
            PrimeNumbers = new LucPrime(p, q);
            P = message;

            if (PrimeNumbers.N <= message)
                throw new ArgumentException("N need to be greater than message");

            PublicKey = new LucPublicKey(GenerateSimpleE(), PrimeNumbers.N);

            PrivateKey = new LucPrivateKey(getSmalld(PublicKey), PrimeNumbers.N);

        }
    }
}