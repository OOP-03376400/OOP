//Sean Wright
//1/24/19
//Version 1
using System;

namespace P2
{
    class Trigger
    {
        //Data members
        private uint PingAttempts;
        private uint MatchingDigits;
        private static uint FirstDig;
        private static uint SecondDig;
        private ClosePrime First;
        private ClosePrime Second;

        //mutators
        private uint GetPingAttempts() => PingAttempts;
        private uint GetMatches() => MatchingDigits;
        private static uint GetFirstDig() => FirstDig;
        private static uint GetSecondDig() => SecondDig;

        private void SetMatches(uint matches) => MatchingDigits = matches;
        private void SetPings(uint pings) => PingAttempts = pings;

        //constructors

        //Preconditions:None
        //Postconditions: Creates 2 randomized numbers for closeprime objects,
        //since we require both objects for Trigger functionality.
        public Trigger()
        {
            Trigger.FirstDig = (uint)rand.Next();
            Trigger.SecondDig = (uint)rand.Next();
            First = new ClosePrime(GetFirstDig());
            Second = new ClosePrime(GetSecondDig());
            PingAttempts = 0;
            MatchingDigits = 0;
        }

        //
        public Trigger(uint FirstDigit, uint SecondDigit)
        {
            First = new ClosePrime(FirstDigit);
            Second = new ClosePrime(SecondDigit);
            PingAttempts = 0;
            MatchingDigits = 0;
        }
        //Private methods
        private static readonly Random rand = new Random();

        //Public methods
        public bool PingTrig(uint PingNum)
        {
            uint FirstStatus = First.GetStatus();
            uint SecondStatus = Second.GetStatus();
            SetPings(GetPingAttempts() + 1);
            if (FirstStatus == 1)
                First.Revive();
            if (SecondStatus == 1)
                Second.Revive();
            uint remainder1 = First.Ping(PingNum) % 10;
            uint remainder2 = Second.Ping(PingNum) % 10;
            if (remainder1 == remainder2)
                Console.WriteLine("Ping of "+PingNum+" resulted in last digit of "+ remainder1);
            bool MatchingDigits = (First.Ping(PingNum) % 10 == Second.Ping(PingNum) % 10);
            if (MatchingDigits)
                SetMatches(GetMatches() + 1);
            return MatchingDigits;

        }
        public void Stats()
        {
            Console.WriteLine("Total Ping Attempts: "+GetPingAttempts()+".");
            Console.WriteLine("Total Matching Pings: " + GetMatches() + ".");
            return;
        }
    }
}
