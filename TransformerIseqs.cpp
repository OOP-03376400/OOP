//TransformerIseqs.cpp
//Sean Wright
//Version 1
//Implementation file for TransformerIseqs class

#include "TransformerIseqs.h"

using namespace std;
int makePos = -1;
TransformerIseqs :: TransformerIseqs() : Iseqs(), Transformer(){}

//Preconditions: Iseqs and Transformer classes have public/protected constructors corresponding to accepting an int and a string
//Postconditions: Will instantiate a Iseq with provided int, a Transformer with provided string, and may instantiate
//a RandomS or OscillateS object if the user provides a specific number (1 = RandomS, 2 = OscillateS, anything else = use base Iseq) 
//If the user does declare a RandomS or OscillateS object then the internal int IseqType changes which is used to tell the destructor
//to delete the respective heap objects.
TransformerIseqs :: TransformerIseqs(int WhichIseqs, int ISeqPValue, string TransWord) : Iseqs(ISeqPValue), Transformer(TransWord){
	switch (WhichIseqs){
		case 0: IseqType = 0;
		
		case 1: 
			{
				Rand = new RandomS(ISeqPValue);
				IseqType = 1;
			}
		case 2: 
			{
				Oscill = new OscillateS(ISeqPValue);
				IseqType = 2;
			}
		default: IseqType = 0;
	}
}

TransformerIseqs :: TransformerIseqs(const TransformerIseqs &a){}

//Preconditions: Needed since objects created on the heap 
//Postconditions: since private, won't ever be called
TransformerIseqs& TransformerIseqs ::operator+=(const TransformerIseqs &a){
		return *this;
	}

//Preconditions: none 
//Postconditions: Will delete Heap allocated objects if they were instantiated
TransformerIseqs :: ~TransformerIseqs(){
	if(IseqType ==1){
		delete Rand;
		Rand = 0;
	}
	if(IseqType ==2){
		delete Oscill;
		Oscill = 0;
	}
}

//Preconditions: Function defined in parent, available 
//Postconditions: none
int TransformerIseqs :: Query(int x){
	switch(IseqType){
		case 0: 
			return Iseqs::Query(x);
		case 1:
			return Rand->Query(x);
		case 2:
			return Oscill->Query(x);
	}
	return 0;
}

//Preconditions: Function defined in parent, available 
//Postconditions: changes the character passed in by reference to help
//know which ascii value correlates to which character
int TransformerIseqs ::  AsciiValue(int RandomNumber, char &y){
	int Value = 0, x = 0;
	switch(IseqType){
		case 0:
			x = Iseqs::Query(RandomNumber);
			
		case 1:
			x = Rand->Query(RandomNumber);
		
		case 2:
		{
			x = Oscill->Query(RandomNumber);
			if(x <0)
				x = x * makePos;
		}
	}
	Value = Transformer::AsciiValue(x,y);
	return Value;
}


//This performs same functionality as AsciiValue above, but is a local function
//in the sense that it's not an inherited function to be overriden

//Preconditions: The respective Iseq object has been created, Transformer has been created.
//The functions Query and AsciiValue are accessable to class
//Postconditions: changes the character passed in by reference to help
//know which ascii value correlates to which character
int TransformerIseqs :: Execute(int RandomNumber, char &y){
	int x = 0;
	int Value =0;
	
	switch(IseqType){
		case 0:
			x = Iseqs::Query(RandomNumber);
			
		case 1:
			x = Rand->Query(RandomNumber);
		
		case 2:
			x = Oscill->Query(RandomNumber) * makePos;
			
	}
	Value = Transformer::AsciiValue(x,y);	
	
	return Value;
}
