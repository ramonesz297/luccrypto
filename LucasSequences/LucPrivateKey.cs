using System.Numerics;
using LucasSequences;

namespace LucasSequences
{
    public struct LucPrivateKey : ILucKey
    {
        public BigInteger d { get; private set; }
        public BigInteger N { get; private set; }


        public LucPrivateKey(BigInteger d, BigInteger n)
        {
            this.d = d;
            this.N = n;
        }

        public LucPrivateKey(LucPublicKey publicKey, LegendreNumbers legendreNumbers)
        {
            d = BigInteger.ModPow(publicKey.e, (int)(legendreNumbers.Sn.Eyler() - 1), legendreNumbers.Sn);

            N = publicKey.N;
        }

    }
}