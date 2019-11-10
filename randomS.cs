using System;

namespace p3
{
    class RandomS : Iseqs
    {
        private readonly uint ArbValue = 7;


        public RandomS()
        {
            ObjectOn = true;
            CurrentValue = Seed;
            P = GenRandomDig();
            NumQueries = 0;
        }
        public RandomS(uint PValue)
        {
            ObjectOn = true;
            CurrentValue = Seed;
            P = PValue%10;
            NumQueries = 0;
        }
        public override uint Query()
        {
            uint next = 0;
            NumQueries++;
            if (ObjectOn)
                next = GenRandomDig() + 2*GenRandomDig());
            if(next % P == 0 && NumQueries != 0)
                next = 0;
            if (next % ArbValue == 0 && next != 0)
                ObjectOn = false;
            UpdateStatus();
            return next;
        }
        
    }
}
