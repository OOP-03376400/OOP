//Sean Wright
//3/9/19
//Version 3
//RandomS.cs child class of Iseqs


//Class Invariant
/*RandomS is the child class of Iseqs, with the caveout being the query function returns a number from a random sequence,
 * unless that number is a multiple of some arbitrary value that is internally set, and is constant. This class
 * provides the same functionality, usability and idea as its parent class. The random sequence is internally 
 * generated and constant.
 */


namespace p6
{
    class RandomS : Iseqs
    {
        
        private readonly uint ArbValue = 3;
        private const int size = 5;
        private int index;
        private readonly int[] arr = {4,16,19,3,11};



        //Preconditions: None
        //Postconditions: Creates RandomS object, status is active with P value of 5
        private RandomS()
        {
            ObjectOn = true;
            P = ArithHop;
            NumQueries = 0;
            index = 0;
        }

        //Preconditions: PValue is non-negative int
        //Postconditions: Creates RandomS object with user input PVlalue, will correct itself if user enters > 9 or 0
        //Initaliized as active
        public RandomS(uint PValue)
        {
            ObjectOn = true;
            P = PValue % makeDigit;
            if (P == 0 | P ==10)
                P = ArithHop;
            index = 0;
            NumQueries = 0;
        }

        //Preconditions: None
        //Postconditions: Will call Iseqs UpdateStatus function aafter the query, and since they perform with the same
        //functionality there's no need to override it. Returns 0 if object is inactive or a  pseudo random number if active
        //Creates new entry in Vtab, overrides Iseqs query function to return a number from a different sequence
        public override void Reset()
        {
            index = 0;
            ObjectOn = true;
            NumQueries = 0;
            return;
        }

        //Preconditions: None
        //Postconditions: Will look at built in array for correct random output, will turn object off if the value at indexed array
        //is a multiple of the ArbValue. Will call update stauts which can change state of object
        public override int Query()
        {
            int next = 0;
            NumQueries++;
            index = index % size;
            if (ObjectOn)
                next = arr[index];
            if (next % P == 0 && NumQueries != 0)
                next = 0;
            if (next % ArbValue == 0 && next != 0)
                ObjectOn = !ObjectOn;
            UpdateStatus();
            index++;
            return next;
        }
        //Since updatestatus function the same no need to override it

    }
}
