//Sean Wright
//3/9/19
//Version 3
//Iseqs.cs main class implementation

/*  Class invariant

Each Iseqs encapsulates an int, P which is used for internal state transistions. This class is used to generate 
numbers from an arithmetic sequence, via the Query function. Each object is initially on but turns to off if 
the number of queries reaches the encapsulated digit that was passed in upon construction. The only public 
functions are construction with an int, Query which returns an int and status which returns if the object
is on. If the object goes from active to inactive the arithmetic sequence is held in place, and any queries 
result in 0. The object changes status everytime the amount of queries is a multiple of the encapsulated digit.
Each object is initiallized with counters set to 0, state change is internallly controlled with added external reset.
Stable hidden digit and constant difference in sequencne hops.
*/


namespace p6
{
    class Iseqs : IIseqs
    {
        //Data members
        protected int Seed = 5;
        protected const int ArithHop = 5;
        protected int CurrentValue;
        protected bool ObjectOn;
        protected const int makeDigit = 10;
        protected uint P;
        protected uint NumQueries;


        //Preconditions: PValue passed in is a non-negative int
        //Postconditions: Creates Iseqs object with a Pvalue provided by user, defaults to 5 if they entered 0
        //Status is active
        public Iseqs(uint PValue)
        {
            ObjectOn = true;
            CurrentValue = Seed;
            P = PValue % makeDigit;
            if (P == 0 | P ==10)
                P = ArithHop;
            
            NumQueries = 0;
        }

        //Preconditions: None
        //Postconditions: Creates Iseqs object with P value of 5 and status is active
        protected Iseqs()
        {
            ObjectOn = true;
            CurrentValue = Seed;
            P = ArithHop;
            NumQueries = 0;
        }

        //Preconditions: None
        //Postconditions: Will return an int, either 0 if object is inactive or the next number in the sequency if
        //the object is active
        public virtual int Query()
        {
            NumQueries++;
            int next = 0;
            if (ObjectOn)
            {
                next = (CurrentValue + ArithHop);
                CurrentValue = next;
            }
            UpdateStatus();
            return next;
        }

        //Preconditions: None
        //Postconditions: Can toggle the active status of the object depending upon number of qureies on the object
        //Creates new Vtab entry
        protected virtual void UpdateStatus()
        {
            if (NumQueries % P == 0 && NumQueries != 0)
            {
                bool status = ObjectOn;
                if (status)
                    ObjectOn = false;
                else
                {
                    ObjectOn = true;
                    NumQueries = 0;
                }
            }
            return;
        }

        //Preconditions: None
        //Postconditions: Fully resets objects status and member counts
        public virtual void Reset()
        {
            ObjectOn = true;
            CurrentValue = Seed;
            NumQueries = 0;
            return;
        }

        //Preconditions: None
        //Postconditions: Returns object status but cannot change it
        public bool Status()
        {
            return ObjectOn;
        }
    }

}
