//Sean Wright
//1/24/19
//Version 1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2
{
    class MultiQ
    {

        //data fields
        private Queue<ClosePrime> queue = new Queue<ClosePrime>();
        private const uint MaxCapicity = 10;
        private uint CurSize;
        private bool Initallized;
        private uint MaxPinged;
        private uint MinPinged;
        private uint TotalPinged;


        //mutators
        private uint GetSize() => CurSize;
        private uint GetTotal() => TotalPinged;
        private uint GetMinPinged() => MinPinged;
        private uint GetMaxPinged() => MaxPinged;
        private bool GetInital() => Initallized;

        private void SetTotal(uint total) => TotalPinged = total;
        private void SetSize(uint size) => CurSize = size;
        private void SetMaxPinged(uint max) => MaxPinged = max;
        private void SetMinPinged(uint min) => MinPinged = min;
        private void SetInital(bool constructed) => Initallized = constructed;

        //constructors

        //Preconditions: None
        //Postconditions: When a MultiQ object is created without parameters, the only function
        //you can perform on that object is the constrcutor function, which acts as the constructor with
        //parameters being passed in does.
        public MultiQ()
        {
            CurSize = 0;
            MaxPinged = 0;
            MinPinged = 40000;
            TotalPinged = 0;
            Initallized = false;
        }

        //Preconditions:None
        //Postconditions: The queue will be created with up to 10 closeprime objects, with positive hidden digits.
        public MultiQ(uint size, Queue<uint> digits)
        {
            if (size > MaxCapicity)
                size = size % 11;
            if (size != (uint)digits.Count)
                size = (uint)digits.Count;
            CurSize = size;
            for (uint i = 0; i < CurSize; i++)
            {
                uint dig = digits.Dequeue();
                ClosePrime add = new ClosePrime(dig);
                queue.Enqueue(add);
            }
            MaxPinged = 0;
            MinPinged = 40000;
            TotalPinged = 0;
            Initallized = true;
        }
        

        //Preconditions: The queue isn't at max capacity.
        //Postconditions: Queue will have 1 more closeprime object.
        public bool Add(uint NewDigit)
        {
            if (Initallized && CurSize <MaxCapicity)
            {
                ClosePrime x = new ClosePrime(NewDigit);
                queue.Enqueue(x);
                return true;
            }
            return false;
        }

        //Preconditions: Will return false if the queue is empty.
        //Postconditions: The queue will remove the oldest closeprime object.
        public bool Remove()
        {
            if(Initallized &&CurSize > 0)
            {
                queue.Dequeue();
                CurSize = GetSize() - 1;
                return true;
            }
            return false;
        }


        //This method is used when you create a queue without any parameters, making it
        //not dependent on ClosePrime instantiation to work.
        //Preconditions: Queue was created using the default (no parameters) constructor.
        //Postconditions: Will update the initiallized status along with creating
        //closeprime objects with hidden digits passed in as a parameter.
        public bool Construct(uint size, Queue<uint> digits)
        {
            if (!Initallized)
            {
                if (size > MaxCapicity)
                    size = size % 11;
                if (size != (uint)digits.Count)
                    size = (uint)digits.Count;
                CurSize = size;

                for (uint i = 0; i < CurSize; i++)
                {
                    uint dig = digits.Dequeue();
                    ClosePrime add = new ClosePrime(dig);
                    queue.Enqueue(add);
                }
                Initallized = true;
                return true;
            }
            return false ;
        }

        //Preconditions: For a non-zero output the queue must have at least 1 closeprime
        //object on it. Pinging a value of 0 is fine because the closePrime class will update it to 5.
        //Postconditions: It will update the data members.
        public uint[] Ping(uint PingNum)
        {
            uint[] values = new uint[2];
            uint value;
            if (Initallized && CurSize >0)
            {
                SetTotal(GetTotal() + 1);
                //I created a list to determine the minimum and maximum return values of this ping along
                //with updating the overall maximum, cumulative and minimum pinged values.
                List<uint> Returnvalues = new List<uint>((int)CurSize);
                for (int i = 0; i < CurSize; i++)
                {
                   ClosePrime x =queue.Dequeue();
                    if (x.GetStatus() == 1)
                        x.Revive();
                    value = x.Ping(PingNum);
                    if (value > GetMaxPinged())
                        SetMaxPinged(value);
                    if (value < GetMinPinged())
                        SetMinPinged(value);
                    SetTotal(GetTotal() + value);
                    //Since it's a queue (FIFO) dequeing everything and enqueuing works well.
                    queue.Enqueue(x);
                    Returnvalues.Add(value);
                }
                values[0] = Returnvalues.Min();
                values[1] = Returnvalues.Max();
            }
            return values;
        }

        //Preconditions: The queue has been initallized.
        //Postconditions: None
        public uint[] Stats()
        {
            const uint ReturnStats = 4;
            uint[] stats = new uint[ReturnStats];
            if (Initallized)
            {
                stats[0] = GetMinPinged();
                stats[1] = GetMaxPinged();
                stats[2] = ((stats[0] + stats[1]) / 2);
                stats[3] = GetTotal();
            }
            return stats;
            
        }
    }
}
