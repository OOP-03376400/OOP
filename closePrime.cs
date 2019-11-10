//Sean Wright
//1/24/19
//Version 3
using System;

namespace P2
{
    public class ClosePrime
    {
        //Data Fields
        private readonly uint HiddenDigit;
        private uint PingCount;
        private uint ObjectStatus;

        //Mutators
        private uint GetHiddenDigit() => HiddenDigit;
        public uint GetStatus() => ObjectStatus;
        private uint GetPingCount() => PingCount;

        private void SetPingCount(uint CurrentPing) => PingCount = CurrentPing;
        private void SetStatus(uint newStatus) => ObjectStatus = newStatus;

        //Constructors

        //Preconditions: None
        //Postconditions: The closeprime object will be created with a positive hidden digit value.
        public ClosePrime(uint randomDigit)
        {
            if (randomDigit > 9)
                randomDigit = randomDigit % 10;
            if (randomDigit == 0)
                randomDigit = 3;
            this.HiddenDigit = randomDigit;
            this.ObjectStatus = 2;
            this.PingCount = 0;
        }

        //Preconditions: None
        //Postconditions: Will create a closeprime object with a hidden digit value of 1.
        public ClosePrime()
        {
            this.HiddenDigit = 1;
            this.ObjectStatus = 2;
            this.PingCount = 0;
        }

        

        //Private functions
        //Preconditions: The closeprime object isn't dead.
        //Postconditions: Will change the objects status to 1 if it's been pinged
        //it's maximum number of times.
        private void UpdateStatus(uint UpdatedPings, uint DigMultiple)
        {
            if (GetStatus() != 0)
            {
                if (UpdatedPings == DigMultiple)
                    SetStatus(1);
            }
            else
                return;
        }

        //Preconditions: None
        //Postconditions: None
        private static uint NextPrime(uint num)
        {
            bool prime = false;
            uint NextPrime = num;
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

        //Preconditions: The object isn't dead or been pinged its maximum number of times.
        //Postconditions: Calls the update status with the new ping count which can make the
        //object go from active to inactive (2 to 1).
        public uint Ping(uint PingValue)
        {
            if (PingValue == 0)
                PingValue = 5;
            switch (GetStatus())
            {
                case 0:
                    return 0;
                case 1:
                    return 0;
                case 2:
                    SetPingCount(GetPingCount() + 1);
                    UpdateStatus(GetPingCount(),GetHiddenDigit()*2);
                    for (int i = 0; i < GetHiddenDigit() + 1; i++)
                        PingValue = (NextPrime(PingValue));
                    return PingValue;
            }
            return 0;
        }

        //Preconditions: None
        //Postconditions: None
        public bool Query() => (GetStatus() == 2) ? true : false;

        //Preconditions: None
        //Postconditions: Can fully deactivate a closeprime object is attempting to revive 
        //an active object, or it can revive an inactive closeprime object if it's pinged its
        //max times, or it does nothing if the object is already permanently deactivated.
        public void Revive()
        {
            switch (GetStatus())
            {
                case 0:
                    return;
                case 1:
                    SetStatus(2);
                    SetPingCount(0);
                    return;
                case 2:
                    SetStatus(0);
                    return;
            }
            return;
        }
        //Preconditions: To reset the count the object must currently be active.
        //Postconditions: Same active status just zero out the ping attempts.
        public bool Reset()
        {
            if (GetStatus() != 0)
            {
                SetPingCount(0);
                UpdateStatus(GetPingCount(),GetHiddenDigit()*2);
                return true;
            }
            else
                return false;
        }

    }
}
