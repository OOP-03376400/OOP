using System;

namespace p3
{
    class Iseqs
    {
        //Data members
        protected uint Seed = 5;
        protected const uint ArithHop = 5;
        protected uint CurrentValue;
        protected bool ObjectOn;
        protected const int RandCeiling = 9;
        protected const int RandFloor = 0;
        protected uint P;
        protected uint NumQueries;

        public Iseqs(uint PValue)
        {
            ObjectOn = true;
            CurrentValue = Seed;
            P = PValue % 10;
            NumQueries = 0;

        }
        public Iseqs()
        {
            ObjectOn = true;
            CurrentValue = Seed;
            P = GenRandomDig();
            NumQueries = 0;

        }

        public virtual uint Query()
        {
            NumQueries++;
            uint next = 0;
            if (ObjectOn)
            {
                next = CurrentValue + ArithHop;
                CurrentValue = next;
            }
            UpdateStatus();
            return next;
        }
        protected uint GenRandomDig()
        {
            uint RandDig = (uint)rand.Next(RandCeiling, RandFloor);
            return RandDig;
        }
        protected Random rand = new Random();

        

        protected virtual void UpdateStatus()
        {
            if (NumQueries % P == 0 && NumQueries !=0)
            {
                bool status = ObjectOn;
                if (status)
                    ObjectOn= false;
                else
                    ObjectOn =true;
            }
            return;
        }

        public bool Status()
        {
            return ObjectOn;
        }

    }

}
