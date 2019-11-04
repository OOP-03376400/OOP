//OscillateS.h
//Sean Wright
//Version 1
//Header file for OscillateS class
#ifndef OSCILLATES_H
#define OSCILLATES_H
#include "Iseqs.h"

//Class invariant
/*
 If ‘off’, an oscillateS acts exactly like an iSeq; 
 otherwise, it alternates the sign of the output number 
 and resets the arithmetic sequence for the first k changes of on/off.
 What that means is when it's on it returns negative, when off it returns 0,
 and to go from off to on it must query as many times as k(generated at construction) * number
 of changes(off->on, on->off). Meaning the more times it's queried (more status changes),
 the longer it will be off. The basic idea is the same as the Iseq invariant in terms of interface.
*/

class OscillateS : public Iseqs{
	public:
		OscillateS(int PValue);
		int Query(int x) override;
		bool Status();
		~OscillateS();
	private:
		OscillateS();
		OscillateS(const OscillateS &a);
		void UpdateStatus() override;
		bool TotalResets(int ResetSeq);
		int NumChanges;
        int ResetSeq;
        int ResetsDone;
        const int MakeNeg = -1;
        int K;
};
#endif