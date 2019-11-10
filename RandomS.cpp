//RandomS.cpp
//Sean Wright
//Version 4
//RandomS implementation file
#include "RandomS.h"

using namespace std;
//See Iseqs.h for class invariant
//Only addition is generating a number that's a multiple of 
//the arb value will turn the object off


RandomS :: RandomS() : Iseqs(){}
RandomS :: RandomS(const RandomS &a) {}

//Preconditions: none
//Postconditions: Will create an RandomS object, make it active, figure out what it's
//encapsulated digit is
RandomS :: RandomS(int PValue) : Iseqs(PValue){
	ObjectOn = true;
	CurrentValue = Seed;
	if (PValue < 1)
		PValue = 5;
	if(PValue %MakeDigit ==0)
		PValue = 3;
	P = PValue % MakeDigit;
	NumQueries = 0;
}
RandomS :: ~RandomS(){
	//Destructor
}

//Preconditions: none
//Postconditions: none
bool RandomS :: Status(){
	return ObjectOn;
}

//Preconditions: none 
//Postconditions: Can flip the status of the object if it meets certain requirements
void RandomS :: UpdateStatus() {
	if (NumQueries % P == 0 && NumQueries != 0)
		ObjectOn = !ObjectOn;
	return;
}

//Preconditions: none
//Postconditions: Will call UpdateStatus which can flip the status of the object
int RandomS :: Query(int x){
	NumQueries++;
	int next = 0;
	if(ObjectOn){
		next = x % RandomCeiling;
		if(next % ArbValue ==0)
			ObjectOn = !ObjectOn;		
	}
	UpdateStatus();
	return next;
}