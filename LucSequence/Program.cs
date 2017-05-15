using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LucSequence
{
    class Program
    {

        public static void StartTest(int size = 4)
        {
            int count = 20;
            BigInteger message = 11111;
            var seqPublic = new LucSequence.LucasSequence(message, 1);
            var random = new Random();
            var stopWatcher = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < count; i++)
            {
                stopWatcher.Restart();
                AAtkin a = new AAtkin(1350, random);
                LucPrime lucPrime = new LucPrime(a.RandomPrime, a.RandomPrime);

                LegendreNumbers legendreNumbers;
                try
                {
                    legendreNumbers = new LucSequence.LegendreNumbers(primeNumbers: lucPrime, message: message);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"p\tq\tnull\tnull\tnull\tnull");
                    continue;
                }


                LucPublicKey publicKey = new LucPublicKey(lucPrime, size);
                LucPrivateKey privateKey = new LucPrivateKey(publicKey, legendreNumbers);

                var ciphertext = seqPublic[publicKey.e] % publicKey.N;

                var seqPrivate = new LucSequence.LucasSequence(ciphertext, 1);

                var result = seqPrivate[privateKey.d] % privateKey.N;
                stopWatcher.Stop();
                Console.WriteLine($"{i + 1}.\t{lucPrime.P}\t{lucPrime.Q}\t{lucPrime.N}\t\t${publicKey.e}\t\t{privateKey.d}\t\t{result}\t\t{stopWatcher.ElapsedMilliseconds}");
            }
        }

        static void Main(string[] args)
        {
            var luc = new LucasSequence(3, 1);

            var stopWatcher = new System.Diagnostics.Stopwatch();
            while (true)
            {
                Console.WriteLine("Write index of Luc sequence");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    stopWatcher.Restart();
                    Console.WriteLine(luc[result]);
                    Console.WriteLine(luc.GetU(result));
                    stopWatcher.Stop();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(stopWatcher.ElapsedTicks);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (input.StartsWith("test"))
                {
                    int size = 4;
                    var stParams = input.Split(' ');
                    if (stParams.Length > 1)
                    {
                        if (!int.TryParse(stParams[1].Replace("-", ""), out size))
                        {
                            size = 4;
                        }
                    }

                    Console.Clear();
                    Console.WriteLine($"index\tp\tq\tN\t\tpublic\t\tprivate\t\tresult\t\t time");
                    Program.StartTest(size);
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (String.IsNullOrEmpty(input) || input != "exit")
                {
                    continue;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exit...");
                    break;
                }
            }
            Console.ReadKey();

        }
    }
}