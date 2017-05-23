using System;
using System.Collections.Generic;
using System.Numerics;

namespace LucSequence
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

                if (index % 2 == 1)
                {
                    index--;
                    index /= 2;
                    array.Add(1);
                }
                else
                {
                    index /= 2;
                    array.Add(0);
                }
            }
            return array;
        }

        public BigInteger CalculateV(BigInteger n)
        {
            if (n==0)
            {
                return 2;
            }
            var k = CalculateCountOfOperation(n);
            BigInteger prev = 2, current = P;
            var enumerator = k.GetEnumerator();

            for (int i = k.Count - 1; i >= 0; i--)
            {
                var currentIndex = k[i];
                if (currentIndex == 0)
                {
                    BigInteger V2tn = ((current * current) - 2);
                    BigInteger Vtn1 = ((current * prev) - P);
                    prev = Vtn1;
                    current = V2tn;

                }
                else if (currentIndex == 2)
                {
                    break;
                }
                else
                {
                    BigInteger Vtn1 = (((P * (current * current)) - (current * prev)) - P);
                    prev = ((current * current) - 2);
                    current = Vtn1;
                }
            }

            return current;
        }


        public BigInteger CalculateU(BigInteger n)
        {
            var k = CalculateCountOfOperation(n);
            BigInteger prev = 0, current = 1;
            var enumerator = k.GetEnumerator();
            if (n == 1) return current;
            if (n == 0) return prev;

            for (int i = k.Count - 1; i >= 0; i--)
            {
                var currentIndex = k[i];
                if (currentIndex == 0)
                {
                    BigInteger V2tn = (9 * (current * current) - 6 * current * prev) / P;
                    BigInteger Vtn1 = current * current - prev * prev;
                    prev = Vtn1;
                    current = V2tn;

                }
                else if (currentIndex == 2)
                {
                    break;
                }
                else
                {
                    BigInteger Vtn1 = (9 * (current * current) - 6 * current * prev) / P;
                    prev = current * current - prev * prev;

                    var tmp = Vtn1;
                    Vtn1 = P * Vtn1 - prev;
                    current = Vtn1;
                    prev = tmp;
                }
            }

            return current;
        }

        public BigInteger GetU(BigInteger result)
        {
            return CalculateU(result);
        }
    }
}
