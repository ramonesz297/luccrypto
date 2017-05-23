using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using LucSequence;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Tests
{
    [TestClass]
    public class ExtensionsTest
    {


        [TestMethod]
        public void LucasSeq()
        {

            BigInteger index = 1103;
            BigInteger module = 4071461;
            BigInteger message = 11111;
            var seqPublic = new LucSequence.LucasSequence(message, 1);

            var result = seqPublic[index] % module;
            Assert.AreEqual<BigInteger>(result, 3975392);

        }


        [TestMethod]
        public void EncryptMessage()
        {
            AAtkin a = new AAtkin(1350);
            LucPrime lucPrime = new LucPrime(a.RandomPrime, a.RandomPrime);
            BigInteger message = 11111;

            LegendreNumbers legendreNumbers = new LucSequence.LegendreNumbers(primeNumbers: lucPrime, message: message);
            LucPublicKey publicKey = new LucPublicKey(lucPrime);
            LucPrivateKey privateKey = new LucPrivateKey(publicKey, legendreNumbers);

            var seqPublic = new LucSequence.LucasSequence(message, 1);

            var ciphertext = seqPublic[publicKey.e] % publicKey.N;

            var seqPrivate = new LucSequence.LucasSequence(ciphertext, 1);

            var result = seqPrivate[privateKey.d] % privateKey.N;

            Assert.AreEqual<BigInteger>(result, message);

        }

        [TestMethod]
        public void DsMessage()
        {
            var p = 3719;
            var lambda = 47;

            BigInteger x = 37;
            var lucasSequence = new LucasSequence(lambda, 1);
            var y = lucasSequence.CalculateV(x)%p;
            var yS = lucasSequence.CalculateU(x)%p;

            //ds

            BigInteger message = 3718;
            var k = 23;

            var r = lucasSequence.CalculateV(k)%p;
            var rS = lucasSequence.CalculateU(k)%p;
            var kInverted2 = ModInverse(k, (p + 1));

            var s = (kInverted2 * (message - (x * r)%(p+1))) % (p + 1);
            var xt = (ModInverse(r, (p + 1)) * (message - k * s)) % (p + 1);

            var m2 = (x * r + k * s) % (p + 1);

            var m3 = (x % (p + 1) * (r % (p + 1)) % (p + 1) + (k % (p + 1)) * (s % (p + 1) ) % (p + 1) )% (p+1);

            //Assert.AreEqual<BigInteger>(x, xt);
            Assert.AreEqual<BigInteger>(message, m2);
            Assert.AreEqual<BigInteger>(message, m3);
            // check ds
            var D =
                (lambda * lambda - 4) % (p);

            var yLucasSequense = new LucasSequence(y, 1);
            var rLucasSequense = new LucasSequence(r, 1);


            var lhs = lucasSequence.CalculateV(message) % (p);
            var rhs = ((yLucasSequense.CalculateV(r) * rLucasSequense.CalculateV(s) +
                        (D * yS * yLucasSequense.CalculateU(r) * rS * rLucasSequense.CalculateU(s))) / 2) % (p);

            var vxr = ((lucasSequence.CalculateV(x*r)*lucasSequence.CalculateV(k*s)+D* lucasSequence.CalculateU(x*r)* lucasSequence.CalculateU(k*s))/2)%(p);

            //var rv = ModInverse((2 * lucasSequence.CalculateV(message) - yLucasSequense.CalculateV(r) * rLucasSequense.CalculateV(s)) * (D * yS * yLucasSequense.CalculateU(r) * rLucasSequense.CalculateU(s)), p) % p;
            //test
            //var Dc = ((2*lhs - yLucasSequense.CalculateV(r) * rLucasSequense.CalculateV(s))/ yS * yLucasSequense.CalculateU(r) * rS * rLucasSequense.CalculateU(s)) % p;


            //Assert.AreEqual<BigInteger>(lhs, rhs);

        }

        [TestMethod]
        public void Lemma2Test()
        {
            int a = 3, m = 4, n = 5, k = 9;

            var lucasSequence = new LucasSequence(a, 1);

            var l = BigInteger.Pow(lucasSequence.CalculateV(k), 2) + lucasSequence.CalculateV(2 * m) +
                    lucasSequence.CalculateV(2 * n);

            var r = lucasSequence.CalculateV(m) * lucasSequence.CalculateV(n) * lucasSequence.CalculateV(k);

            Assert.AreEqual(l, r);
        }

        [TestMethod]
        public void T14()
        {
            var lucasSequence = new LucasSequence(3,1);
            int n = 56, k = 73;
            var vnLucasSequence = new LucasSequence(lucasSequence.CalculateV(n), 1);
            var vkLucasSequence = new LucasSequence(lucasSequence.CalculateV(k), 1);
            Assert.AreEqual(lucasSequence.CalculateV(56*73),vkLucasSequence.CalculateV(n));
            Assert.AreEqual(lucasSequence.CalculateV(56*73),vnLucasSequence.CalculateV(k));

            var unLucasSequence = new LucasSequence(lucasSequence.CalculateU(n), 1);
            var ukLucasSequence = new LucasSequence(lucasSequence.CalculateU(k), 1);

            Assert.AreEqual(lucasSequence.CalculateU(56 * 73), ukLucasSequence.CalculateU(n));
            Assert.AreEqual(lucasSequence.CalculateU(56 * 73), unLucasSequence.CalculateU(k));
        }

        [TestMethod]
        public void Ds2Message()
        {
            AAtkin atkin = new AAtkin(23);
            LucPrime lucPrime = new LucPrime(atkin.RandomPrime, atkin.RandomPrime);

            LucPublicKey publicKey = new LucPublicKey(lucPrime);
            //int a = 3;

            var m = 17;

            LegendreNumbers legendreNumbers = new LucSequence.LegendreNumbers(primeNumbers: lucPrime, message: m);
            //LucPrivateKey privateKey = new LucPrivateKey(publicKey, legendreNumbers);

            BigInteger a = legendreNumbers.D;
            BigInteger up = (a * a - 4) / lucPrime.P;
            BigInteger uq = (a * a - 4) / lucPrime.Q;

            var x = new Random().Next((lucPrime.P - 1) * (lucPrime.Q - 1) - 1);

            var modulus = (lucPrime.P - up) * (lucPrime.Q - uq);

            var lucasSequence = new LucasSequence(a, 1);
            var y = lucasSequence.CalculateV(x);


            var k = GetGCDk(lucPrime, up, uq);
            var r = lucasSequence.CalculateV(k);
            var s = (ModInverse(k, (lucPrime.P - up) * (lucPrime.Q - uq)) * (m - x * r)) %
                    ((lucPrime.P - up) * (lucPrime.Q - uq));

            var m2 = (x * r + k * s) %
                     ((lucPrime.P - up) * (lucPrime.Q - uq));

            var rLucasSequence = new LucasSequence(r, 1);
            var yLucasSequence = new LucasSequence(y, 1);
            var S = rLucasSequence.CalculateV(s);

            //checking
            var sma = lucasSequence.CalculateV(m) % modulus;
            var sry = yLucasSequence.CalculateV(r) % modulus;

            var S1 = S % modulus;

            var lhs = (sma * sma + sry * sry +
                      S1 * S1 - (4 % modulus)) % modulus;

            var rhs = (S1 * sry * sma) % modulus;

            Assert.AreEqual(lhs, rhs);

        }

        [TestMethod]
        public void Ds3Message()
        {


        }
        private BigInteger GetGCDk(LucPrime lucPrimeNumbers, BigInteger up, BigInteger uq)
        {
            Random r = new Random();
            BigInteger e = 0;
            BigInteger t = BigInteger.Multiply(lucPrimeNumbers.P - up, lucPrimeNumbers.Q - uq);

            do
            {
                e = r.Next(1000, 10000);
            } while (BigInteger.GreatestCommonDivisor(e, t) != 1);

            return e;
        }


        [TestMethod]
        public void PrivateKey()
        {
            LucPrime lucPrime = new LucPrime(p: 1949, q: 2089);
            BigInteger message = 11111;

            LucPublicKey publicKey = new LucPublicKey(1103, 4071461);
            LegendreNumbers legend = new LucSequence.LegendreNumbers(lucPrime, message);
            LucPrivateKey key = new LucPrivateKey(publicKey, legend);

            Assert.AreEqual<BigInteger>(key.d, 24017);
        }

        [TestMethod]
        public void PublicKeyGeneration()
        {
            LucPrime lucPrime = new LucPrime(p: 1949, q: 2089);
            BigInteger message = 11111;

            LucPublicKey key = new LucPublicKey(lucPrime);
            Assert.AreEqual<BigInteger>(MathNet.Numerics.Euclid.GreatestCommonDivisor((lucPrime.P - 1) * (lucPrime.P + 1) * (lucPrime.Q - 1) * (lucPrime.Q + 1), key.e), 1);
        }

        [TestMethod]
        public void LegandNumbers()
        {
            //int
            LucPrime lucPrime = new LucPrime(p: 1949, q: 2089);
            BigInteger message = 11111;

            LegendreNumbers legend = new LucSequence.LegendreNumbers(lucPrime, message);

            Assert.AreEqual<Int32>(lucPrime.N, 4071461);
            Assert.AreEqual<BigInteger>(legend.D, 123454317);
            Assert.AreEqual<int>(legend.Dp, -1);
            Assert.AreEqual<int>(legend.Dq, -1);
            Assert.AreEqual(legend.Sn, 407550);
        }

        [TestMethod]
        public void TestCrypt()
        {
            AAtkin primes = new AAtkin(2000);
            LucPrime lucPrime = new LucPrime(primes.RandomPrime, primes.RandomPrime);
            BigInteger messsage = 11111;
            LucKeyGenerator keyGen = new LucKeyGenerator(lucPrime, messsage);
            var lucSeq = new LucasSequence(messsage, 1);
            var cryptText = lucSeq[keyGen.PublicKey.e] % keyGen.PublicKey.N;

            var lucSeq2 = new LucasSequence(cryptText, 1);
            var decryptText = lucSeq[keyGen.PrivateKey.d] % keyGen.PrivateKey.N;
            Assert.AreEqual<BigInteger>(decryptText, 11111);
        }




        [TestMethod]
        public void TestIsPerfectSquare()
        {
            BigInteger test1 = 9;
            BigInteger test2 = 123;
            Assert.IsFalse(test2.IsPerfectSquare());
            Assert.IsTrue(test1.IsPerfectSquare());
        }


        /// <summary>
        /// Мультипликативное инвертирование по модулю с использованием расширенного алгоритма Евклида.
        /// </summary>
        /// <param name="a">Входящее число</param>
        /// <param name="module">Модуль</param>
        /// <returns></returns>
        public static BigInteger ModInverse(BigInteger a, BigInteger module)
        {
            BigInteger i = module, v = 0, d = 1;
            while (a > 0)
            {
                var t = i / a;
                var x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= module;
            if (v < 0) v = (v + module) % module;
            return v;
        }

    }
}
