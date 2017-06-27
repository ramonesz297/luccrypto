using System;
using System.Numerics;

namespace LucasSequences
{
    public class PrimeGererator
    {

        private readonly BigInteger _message;

        private readonly AAtkin _aatkin;


        public PrimeGererator(BigInteger message)
        {
            _message = message;
            _aatkin = new AAtkin((int)Math.Sqrt((long)message));
        }
        public PrimeGererator(string message)
        {
            _message = new BigInteger(System.Text.Encoding.UTF8.GetBytes(message));
            _aatkin = new AAtkin((int)Math.Sqrt((long)_message));
        }
        public LucPrime GeneratePrime()
        {
            int N = 0, p = 0, q = 0;
            do
            {
                p = _aatkin.RandomPrime;
                q = _aatkin.RandomPrime;
                N = q * p;

            } while (N <= _message);

            return new LucPrime(p, q);
        }
    }
}
