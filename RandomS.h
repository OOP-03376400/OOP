//RandomS.h
//Sean Wright
//Version 1
//Header file for RandomS class
#ifndef RANDOMS_H
#define RANDOMS_H
#include "Iseqs.h"
//Class invariant
/*
Same interface as Iseq, the differences is instead of query returning an int from an arithemtic sequence,
it's a random sequence and if the int returned is a multiple of an arbitrary value then it'll change status.
*/

class RandomS: public Iseqs{
	public:
		RandomS(int PValue);
		int Query(int x) override;
		bool Status();
		~RandomS();
	private:
		const int RandomCeiling = 11;
		RandomS();
		RandomS(const RandomS &a);
		void UpdateStatus();
		const int ArbValue = 6;
};
#endif