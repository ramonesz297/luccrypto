using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void IsPerferScuare()
        {

            BigInteger test1 = 9;
            BigInteger test2 = 111;
            Assert.IsTrue()
            

        }

        [TestMethod]
        public void TestMethod1()
        {

            int Eyler(int n)
            {

                int res = n, en = (n >>= 1) + 1;
                for (int i = 2; i <= en; i++)
                    if ((n % i) == 0)
                    {
                        while ((n % i) == 0)
                            n /= i;
                        res -= (res / i);
                    }
                if (n > 1) res -= (res / n);
                return res;
            }


            BigInteger e = 1103;
            BigInteger m = 407550;
            int r = Eyler((int)m);

            BigInteger d = BigInteger.Pow(e, r-1) % m;

            Assert.IsTrue(d == 24017);

        }
    }
}
