using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using LucSequence;
using System.Collections;
using System;
using System.Text;
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
            //AAtkin a = new AAtkin(1350);
            //LucPrime lucPrime = new LucPrime(a.RandomPrime, a.RandomPrime);
            var p = 13;
            var lambda = 3;
            //LegendreNumbers legendreNumbers = new LucSequence.LegendreNumbers(primeNumbers: lucPrime, message: message);
            //LucPublicKey publicKey = new LucPublicKey(lucPrime);
            //LucPrivateKey privateKey = new LucPrivateKey(publicKey, legendreNumbers);
            BigInteger x = 5;
            var lucasSequence = new LucasSequence(lambda, 1);
            var y = lucasSequence.CalculateV(x) % p;
            var yS = lucasSequence.CalculateU(x) % p;

            //ds

            BigInteger message = 11111;
            var k = 7;

            var r = lucasSequence.CalculateV(k) % p;
            var rS = lucasSequence.CalculateU(k) % p;

            var s = (BigInteger.Pow(k, -1.Eyler()) * (message - x * r)) % (p + 1);


            // check ds
            var D = (lambda * lambda - 4) % p;

            var yLucasSequense = new LucasSequence(y, 1);
            var rLucasSequense = new LucasSequence(y, 1);

            var lhs = lucasSequence.CalculateV(message) % p;
            var rhs = ((yLucasSequense.CalculateV(r) * rLucasSequense.CalculateV(s) + (D * yS * yLucasSequense.CalculateU(r) * rS * rLucasSequense.CalculateU(s))) / 2) % p;

            Assert.AreEqual<BigInteger>(lhs, rhs);
            //var seqPublic = new LucSequence.LucasSequence(message, 1);

            //var ciphertext = seqPublic[publicKey.e] % publicKey.N;

            //var seqPrivate = new LucSequence.LucasSequence(ciphertext, 1);

            //var result = seqPrivate[privateKey.d] % privateKey.N;

            //Assert.AreEqual<BigInteger>(result, message);

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
    }
}
