﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
namespace LucSequence
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


    public static class Extensions
    {
        public static bool IsPerfectSquare(this BigInteger number)
        {
            Double result = Math.Pow(Math.E, BigInteger.Log(number) / 2);

            return result == (int)result;

        }
        public static IEnumerable<int> GeneratePrime(int count)
        {
            var list = new List<int>();
            int i = 2;
            while (1 <= count)
            {
                if (list.Any())
                {

                }
                else
                {
                    i += 0;
                }

            }
            return null;
        }

        public static long Eyler(this long n)
        {
            long res = n, en = Convert.ToInt64(Math.Sqrt(n) + 1);
            for (long i = 2; i <= en; i++)
                if ((n % i) == 0)
                {
                    while ((n % i) == 0)
                        n /= i;
                    res -= (res / i);
                }
            if (n > 1) res -= (res / n);
            return res;
        }



        public static int Eyler(this int n)
        {
            int res = n, en = Convert.ToInt32(Math.Sqrt(n) + 1);
            for (var i = 2; i <= en; i++)
                if ((n % i) == 0)
                {
                    while ((n % i) == 0)
                        n /= i;
                    res -= (res / i);
                }
            if (n > 1) res -= (res / n);
            return res;
        }


        public static BigInteger Lmc(this BigInteger a, BigInteger b)
        {

            BigInteger max = BigInteger.Max(a, b);

            BigInteger hcf = 1;
            BigInteger temp = b;

            while (hcf != temp)
            {
                if (hcf > temp)
                    hcf -= temp;
                else
                    temp -= hcf;
            }

            return (a * b) / hcf;
        }

        public static BigInteger Gcd(this BigInteger a, BigInteger b)
        {
            BigInteger nod = 1;
            BigInteger tmp;
            if (a == 0)
                return b;
            if (b == 0)
                return a;
            if (a == b)
                return a;
            if (a == 1 || b == 1)
                return 1;
            while (a != 0 && b != 0)
            {
                if (((a & 1) | (b & 1)) == 0)
                {
                    nod <<= 1;
                    a >>= 1;
                    b >>= 1;
                    continue;
                }
                if (((a & 1) == 0) && (b & 1) == 0)
                {
                    a >>= 1;
                    continue;
                }
                if ((a & 1) == 0 && ((b & 1) == 0))
                {
                    b >>= 1;
                    continue;
                }
                if (a > b)
                {
                    tmp = a;
                    a = b;
                    b = tmp;
                }
                tmp = a;
                a = (b - a) >> 1;
                b = tmp;
            }
            if (a == 0)
                return nod * b;
            else
                return nod * a;

        }
        public static int BinaryPow(this int a, int n)
        {
            int result = 1;

            while (n > 0)
            {
                if ((n & 1) != 0)
                {
                    result *= a;
                }
                a *= a;
                n >>= 1;
            }
            return result;
        }
    }
}
