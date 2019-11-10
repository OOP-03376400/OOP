//Iseqs.cpp
//Sean Wright
//Version 4
//Main Isequence file
#include "Iseqs.h"

using namespace std;

Iseqs :: Iseqs(){}
Iseqs :: Iseqs(const Iseqs &a){}

//Preconditions: none
//Postconditions: Creates an Iseq object, set to active with an encapsulated digit
Iseqs :: Iseqs(int PValue)
{ 
	ObjectOn = true;
	CurrentValue = Seed;
	if (PValue < 1)
		PValue = 5;
	if(PValue %MakeDigit ==0)
		PValue = 7;
	P = PValue % MakeDigit;
	NumQueries = 0;
}

//Preconditions: none
//Postconditions: Can flip object status if the conditional is met
void Iseqs :: UpdateStatus(){
	if (NumQueries % P == 0 && NumQueries != 0)
		ObjectOn = !ObjectOn;
	return;
}

Iseqs :: ~Iseqs(){
	//May need to flush out
}

//Preconditions: none 
//Postconditions: will call updatestatus which can flip status of object
int Iseqs :: Query(int x){
	NumQueries++;
	//X is only needed for
	//RandomS sequence generation
	int next = 0;
	if (ObjectOn)
	{
		next = (CurrentValue + ArithHop);
		CurrentValue = next;
	}
	UpdateStatus();
	return next;
	
}

//Preconditions: none 
//Postconditions: none 
bool Iseqs :: Status(){
	return ObjectOn;
}