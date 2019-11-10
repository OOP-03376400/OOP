//Sean Wright
//1/13/19
//Visual Studio 2017 version 15.9.5
using System;

namespace go
{
    public class ClosePrime
    {
        //Data Fields
        private readonly int HiddenDigit;
        private int PingCount;
        private int ObjectStatus;

        //Constructors
        public ClosePrime(int randomDigit)
        {
            //If int passed in isn't a positive digit, turns input into a good value
            if (randomDigit < 1 || randomDigit > 10)
                randomDigit = Math.Abs(randomDigit) % 10;
            this.HiddenDigit = randomDigit;
            this.ObjectStatus = 2;
            this.PingCount = 0;
        }
        public ClosePrime()
        {
            this.HiddenDigit = 1;
            this.ObjectStatus = 2;
            this.PingCount = 0;
        }

        //Mutators
        private int GetHiddenDigit() => HiddenDigit;
        private int GetStatus() => ObjectStatus;
        private int GetPingCount() => PingCount;
        private void SetPingCount(int CurrentPing) => PingCount = CurrentPing;
        private void SetStatus(int newStatus) => ObjectStatus = newStatus;

        //Private functions
        private void UpdateStatus(int UpdatedPings)
        {
            if (GetStatus() != 0)
            {
                int MaxPings = GetHiddenDigit() * 2;
                if (UpdatedPings < MaxPings)
                    SetStatus(2);
                else
                    SetStatus(1);
            }
            else
                return;
        }
        private static int NextPrime(int num)
        {
            bool prime = false;
            int NextPrime = num;
            while (!prime && NextPrime < num * 2)
            {
                NextPrime++;
                prime = true;
                for (int i = 2; i < NextPrime; i++)
                {
                    if (NextPrime % i == 0)
                    {
                        prime = false;
                        break;
                    }
                }
            }
            return NextPrime;
        }

        //Public functions
        public int Ping(int PingValue)
        {
            if (PingValue < 1)
                PingValue = Math.Abs(PingValue);
            int CurrentPing = GetPingCount();
            switch (GetStatus())
            {
                case 0:
                    return 0;
                case 1:
                    return 0;
                case 2:
                    SetPingCount(CurrentPing+1);
                    UpdateStatus(GetPingCount());
                    for (int i = 0; i < GetHiddenDigit() + 1; i++)
                        PingValue = NextPrime(PingValue);
                    return PingValue;
            }
            return 0;
        }
        
        public bool Query() => (GetStatus() == 2) ? true : false;
        
        public void Revive()
        {
            Console.Write("Attempting to revive the object. ");
            switch (GetStatus())
            {
                case 0:
                    Console.Write("This object is permanently inactive and cannot be revived.");
                    break;
                case 1:
                    Console.Write("Object has been revived, the ping counter has been reset.");
                    SetStatus(2);
                    SetPingCount(0);
                    break;
                case 2:
                    Console.WriteLine("Object is now permanently inactive due to reviving while being active.");
                    SetStatus(0);
                    break;
            }
        }
        //To use reset the object must currently be active
        public void Reset()
        {
            if (GetStatus() ==2)
            {
                SetPingCount(0);
                UpdateStatus(GetPingCount());
                Console.WriteLine("Ping counter reset.");
                return;
            }
            else
                return;
        }     

    }
}
