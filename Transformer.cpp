//Transformer.cpp
//Sean Wright
//Version 1
//Implementation file for Transformer
#include "Transformer.h"
#include <cstdlib>


Transformer :: Transformer(){}

//Preconditions: None 
//Postconditions: Generates Transformer object, fills out data members with
//encapsulated word and the length of it
Transformer :: Transformer(string w){
	Word = w;
	WordLength = sizeof(Word);
	
}

Transformer :: ~Transformer(){}
Transformer ::Transformer (const Transformer &a){}

//Preconditions: none 
//Postconditions: Changes the character passed in to reflect the 
//character corresponding to the ascii value returned
int Transformer :: AsciiValue(int x, char &y){
	char Character;
	if(x <0)
		x = abs(x);
	x = x%WordLength;
	Character = Word[x];
	x = (int)Character;
	y = Character;
	return x;
}
