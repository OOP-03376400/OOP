using System;

namespace go
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program is an exercise in encapsulation and creating a class in C#.");
            Console.WriteLine("Each object has a positive digit that is generated randomly and not shown.");
            Console.WriteLine("Every object can be pinged up to twice as many times as the hidden digit, at");
            Console.WriteLine("which point the object will become inactive. The available functions are:");
            Console.WriteLine("ping, IsActive, reset, revive. Ping returns the prime that's hidden digit spots");
            Console.WriteLine("ahead of (counting primes) the ping value. Ping will convert negative ints to positive");
            Console.WriteLine("if necessary. IsActive returns the status of that object, either active or inactive.");
            Console.WriteLine("The reset function sets the ping counter to 0 only if it's currently an active object.");
            Console.WriteLine("Revive changes the status of the object to active if it's been pinged the max number of");
            Console.WriteLine("times, or permanently inactive if reviving an active object. Reviving a permanently");
            Console.WriteLine("inactive object does nothing. There will be 10 objects created and manipulated, each");
            Console.WriteLine("object will be pinged, status queried, revived, queried some more, pinged, counters reset,");
            Console.WriteLine("etc. to demonstrate full funtionality. Only one object will be displayed at a time.");

            //I decided to set the max ping attempts to 25, but the program will reset the counter after 10 pings
            //if it's full, otherwise it waits to reset the counter at 20. This gives a chance to transition some of the objects
            //from active to inactive, but others should have a ping capactity above 10 so it may not kick in until the 20th ping.
            //The break point is where I reset the ping counter, then revive it which makes it permanently inactive. 
            //The ping values range from 1-1000, but the ping function converts negative numbers in case the client tries to be tricky,
            //which means the prime finder only looks for increasing primes, not decreasing. 
            const int ArrSize = 10, BreakPoint = 23;
            const int DigMin = 1, DigMax = 10, PingMin = 1, PingMax = 1000, TestPings = 25;

            ClosePrime[] ArrayObjects = new ClosePrime[ArrSize];
            for (int i = 0; i < ArrSize; i++)
            {
                Console.WriteLine(" \nTesting object: " + (i + 1) + ":");
                int RandomDigit = rand.Next(DigMin, DigMax);
                bool active;
                ArrayObjects[i] = new ClosePrime(RandomDigit);
                active = IsActive(ArrayObjects[i]);
                for (int j = 0; j < TestPings; j++)
                {
                    int RandomPing = rand.Next(PingMin, PingMax);
                    int ReturnPrime = ArrayObjects[i].Ping(RandomPing);
                    if (ReturnPrime > 1)
                        Console.WriteLine("Pinged " + RandomPing + " and received " + ReturnPrime + ".");
                    else
                        Console.WriteLine("Cannot ping, the object is inactive.");
                    if ((j % 10 == 0) && (j != 0))
                    {
                        active = IsActive(ArrayObjects[i]);
                        if (!active)
                        {
                            Revive(ArrayObjects[i]);
                            active = IsActive(ArrayObjects[i]);
                        }
                    }
                    if (j == BreakPoint)
                    {
                        Console.WriteLine("Now testing reviving an active object.");
                        active = IsActive(ArrayObjects[i]);
                        Reset(ArrayObjects[i]);
                        active = IsActive(ArrayObjects[i]);
                        Revive(ArrayObjects[i]);
                    }
                }
                Console.WriteLine("Press enter to terminate the program.");
                Console.ReadLine();
            }
            Console.ReadKey();
        }

        private static Random rand = new Random();
        static bool IsActive(ClosePrime obj)
        {
            if (obj.Query())
            {
                Console.WriteLine("Object is active.");
                return true;
            }
            else
            {
                Console.WriteLine("Object is inactive.");
                return false;
            }
        }

        static void Reset(ClosePrime obj) => obj.Reset();
        static void Revive(ClosePrime obj) => obj.Revive();
    }
}
