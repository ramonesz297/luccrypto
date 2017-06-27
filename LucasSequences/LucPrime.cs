namespace LucasSequences
{
    public struct LucPrime
    {
        public int P { get; private set; }

        public int Q { get; private set; }

        public int N => Q * P;

        public LucPrime(int p, int q)
        {
            P = p;
            Q = q;
        }

    }
}
