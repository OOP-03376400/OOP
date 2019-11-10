//Iseqs.h
//Sean Wright
//Version 1
//Header file for Iseqs class
#ifndef ISEQS_H
#define ISEQS_H


//Class invariant:
/*
This class is used to generate numbers from an arithmetic sequence. Each object is
initally on but turns to off if the number of queries reaches the encapsulated digit
that was passed in upon construction. The only public functions are construction with an int,
Query which returns an int and status which returns if the object is on. If the object goes from
active to inactive the arithmetic sequence is held in place, and any queries result in 0.
The object changes status everytime the amount of queries is a multiple of the encapsulated digit.
*/

class Iseqs{
	public:
		Iseqs(int PValue);
		virtual ~Iseqs();
		virtual int Query(int x);
		bool Status();
		
	protected:
		const int Seed = 5;
		const int ArithHop = 5;
		int CurrentValue;
		const int MakeDigit = 10;
		bool ObjectOn;
		int P;
		int NumQueries;
		virtual void UpdateStatus();
		Iseqs();
		Iseqs(const Iseqs &a);
};
#endif