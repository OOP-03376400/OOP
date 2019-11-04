//Transformer.h
//Sean Wright
//Version 1
//Header file for Transformer class

#ifndef TRANSFORMER_H
#define TRANSFORMER_H
#include <string>
//Class Invariant
/*
The Transformer class encapsulates a string upon construction, and provides the user the function AsciiValue.
That function returns the ascii value of the xth character in the encapsulated string, where x is an int 
passed in. If x is larger than the length of the string it will autocorect.
*/
using namespace std;
class Transformer{
	public:
		//
		Transformer(string w);
		virtual ~Transformer();
		virtual int AsciiValue(int x, char &y);
	private:
		//
		string Word;
		int WordLength;
	protected:
		//
		Transformer();
		Transformer(const Transformer &a);
}; 
#endif