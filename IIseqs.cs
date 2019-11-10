//Sean Wright
//IIseqs.cs
//3/15/19
//Version 1

//Class invariant
//SInce it's just an interface refer to Iseqs invariant for methods, state control etc.
namespace p6
{
    interface IIseqs
    {
        int Query();
        void Reset();
        bool Status();
    }
}