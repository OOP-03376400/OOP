//Sean Wright
//3/9/19
//Version 3
//OscillateS.cs Child class of Iseqs

/*Class invariant
 * This class is the child class of Iseqs, but differs in a few ways. The query function returns the same sequence, but the negative value. It also maintains place in the sequence
 * if it becomes inactive, which also is different. Instead of switching states every multiple of P Queries, now there's an int k which is used in combination with a counter of total number
 * of state changes to calculate how many pings are needed until Query returns a number from the sequence. While it's in this state it returns 0, along with when it's off. This added
 * twist means a new function is necessary to see if the number of queries hits the request number, and it's provided in the TotalResets function.
 * Each object is initiallized with counters at 0, internally triggered state change with external reset, similar to Iseqs.
 * Stable hidden digit, k value, and unlimited state changes.
 */



namespace p6
{
    class OscillateS : Iseqs
    {
        //Data members
        private uint NumChanges;
        private uint ResetSeq;
        private int ResetsDone;
        private readonly uint K;

        //Preconditions: None
        //Postconditions: Creates oscillateS object with a P value of 5
        //status is active

        private OscillateS()
        {
            ObjectOn = true;
            NumChanges = 0;
            CurrentValue = Seed;
            //K will be generated differently for each OscillateS object created
            P = ArithHop;
            ResetsDone = 0;
            NumQueries = 0;
            K = P % ArithHop;
        }

        //Preconditions:PValue is a non-negative int
        //Postconditions: Creates oscillateS object with a P value  digit based on user input
        //status is active

        public OscillateS(uint PValue) 
        {
            ObjectOn = true;
            NumChanges = 0;
            ResetsDone = 0;
            CurrentValue = Seed;
            P = PValue % makeDigit;
            if (P ==0 | P ==10 )
                P = ArithHop;
            
            K = P % ArithHop;
            NumQueries = 0;
        }

        //Preconditions: OscillateS object has been created
        //Postconditions: Keeps track of how many resets done to return to normal functionality
        //Returns 0 if object is off, also Creates new entry in Vtab overwritting Iseqs Query function
        public override int Query()
        {
            NumQueries++;
            ResetSeq = K * NumChanges;
            int next = 0;
            if (ObjectOn)
            {
                if (TotalResets(ResetSeq))
                {
                    next = (CurrentValue + ArithHop);
                    CurrentValue = next;
                }
                else
                {
                    ResetsDone++;
                    CurrentValue = Seed;
                    next = CurrentValue;
                }
            }
            UpdateStatus();
            return next* -1;
        }

        //Preconditions: None
        //Postconditions: Will resume sequence once the total number of Resets output reaches the required number
        //Used to keep track of if the number of reset queries hits k*number of status changes
        private bool TotalResets(uint ResetSeq)
        {
            bool totalComplete = true;
            if(ResetsDone == ResetSeq)
            {
                NumChanges =0;
                ResetsDone = 0;
                return totalComplete;
            }
            else
                return !totalComplete;
        }

        //Preconditions: None
        //Postconditions: Resets all data members and activates object
        public override void Reset()
        {
            NumQueries = 0;
            NumChanges = 0;
            CurrentValue = Seed;
            ObjectOn = true;
            return;
        }

        //Preconditions: None
        //Postconditions: Will toggle the status of the object if the number of qureries reaches a multiple of P
        //New entry in Vtab overwritting Iseqs UpdateStatus function due to need to keep track off number of status changes
        protected override void UpdateStatus()
        {
            if (NumQueries % P == 0 && NumQueries != 0)
            {
                NumChanges++;

                bool status = ObjectOn;
                if (status)
                    ObjectOn = false;
                else
                    ObjectOn = true;
            }
            return;
        }
    }
}
