using System;
using System.Collections.Generic;
using System.Numerics;

namespace LucasSequences
{
    public class LucasSequence
    {
        /// <summary>
        /// Message 
        /// </summary>
        private readonly BigInteger P;

        /// <summary>
        /// Default is 1
        /// </summary>
        private readonly int Q;

        public BigInteger this[BigInteger index, BigInteger mod]
        {
            get
            {
                if (index < 0) return 0;
                return CulculateVMod(index, mod);
            }
        }

        public BigInteger this[BigInteger index]
        {
            get
            {
                if (index < 0) return 0;
                return CalculateV(index);
            }
        }

        public LucasSequence(BigInteger p, int q)
        {
            P = p;
            Q = q;
        }
        private List<int> CalculateCountOfOperation(BigInteger index)
        {
            List<int> array = new List<int>() { 2 };

            while (index > 1)
            {
                if ((index & 1) == 1)
                {
                    index--;
                    index >>= 1;
                    array.Add(1);
                }
                else
                {
                    index >>= 1;
                    array.Add(0);
                }
            }
            return array;
        }

        private BigInteger CulculateVMod(BigInteger n, BigInteger mod)
        {
            var k = CalculateCountOfOperation(n);
            BigInteger prev = 2, current = P;
            var enumerator = k.GetEnumerator();

            for (int i = k.Count - 1; i >= 0; i--)
            {
                var currentIndex = k[i];
                if (currentIndex == 0)
                {
                    BigInteger V2tn = ((current * current) - 2); // caculate V_2n
                    BigInteger Vtn1 = ((current * prev) - P); // caculate V_(2n-1)
                    prev = Vtn1 % mod;
                    current = V2tn % mod;
                }
                else if (currentIndex == 1)
                {
                    BigInteger Vtn1 = (((P * (current * current)) - (current * prev)) - P); // caculate V_2n+1
                    prev = ((current * current) - 2) % mod; // caculate V_2n
                    current = Vtn1 % mod;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        public BigInteger CalculateV(BigInteger n)
        {
            var k = CalculateCountOfOperation(n);
            BigInteger prev = 2, current = P;
            var enumerator = k.GetEnumerator();

            for (int i = k.Count - 1; i >= 0; i--)
            {
                var currentIndex = k[i];
                if (currentIndex == 0)
                {
                    BigInteger V2tn = ((current * current) - 2); // caculate V_2n
                    BigInteger Vtn1 = ((current * prev) - P); // caculate V_(2n-1)
                    prev = Vtn1;
                    current = V2tn;

                }
                else if (currentIndex == 1)
                {
                    BigInteger Vtn1 = (((P * (current * current)) - (current * prev)) - P); // caculate V_2n+1
                    prev = ((current * current) - 2); // caculate V_2n
                    current = Vtn1;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

    }
}
