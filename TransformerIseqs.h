//TransformerIseqs.h
//Sean Wright
//Version 1
//Header file for TransformerIseqs class
#ifndef TRANSFORMERISEQS_H
#define TRANSFORMERISEQS_H

#include "Transformer.h"
#include "Iseqs.h"
#include "RandomS.h"
#include "OscillateS.h"
//Class invariant 
/*
See Iseqs and Transformer for basic class invariants.
Only addition is Execute which performs the job of generating an int (from Iseq-type obj)
then feeding that int into a Transformer object to run the AsciiValue function.

Another important detail is for the constructor, the user needs 3 fields:
1:Define which type of Iseq object; 0 = Iseq, 1 = RandomS, 2 = OscillateS. If it's a different number
it's assumed to be a general Iseq number generation, so that way it consumes less memory. 
*/
class TransformerIseqs : public Iseqs, public Transformer{
	public:
		TransformerIseqs(int WhichIseqs, int ISeqPValue, string TransWord);
		int Execute(int RandomNumber, char &y);
		int Query(int x) override;
		~TransformerIseqs();
		char ReturnLastChar(char c);
		int AsciiValue(int x, char &y) override;
	private:
		TransformerIseqs(); // : Iseqs(), Transformer();
		TransformerIseqs(const TransformerIseqs &a);
		TransformerIseqs& operator+=(const TransformerIseqs &a);
		RandomS* Rand;
		OscillateS* Oscill;
		int IseqType;
};
#endif


