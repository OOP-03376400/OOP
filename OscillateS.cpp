//OscillateS.cpp
//Sean Wright
//Version 4
//OscillateS implementation filebuf
#include "OscillateS.h"

using namespace std;

OscillateS :: OscillateS() : Iseqs() {}
OscillateS :: OscillateS(const OscillateS &a) {}

//Preconditions: Iseqs has a protected/public constructor that takes an int
//Postconditions: Will fill out data members, set status to active without incrementing 
//number of status changes
OscillateS :: OscillateS(int PValue) : Iseqs(PValue){
	ObjectOn = true;
	NumChanges = 0; 
	ResetsDone = 0;
	CurrentValue = Seed; 
	if (PValue < 1 )
		PValue = 7;
	if (PValue %MakeDigit == 0)
		PValue = 6;
	P = PValue %MakeDigit;
	K = P + 4;
	NumQueries = 0;
	ResetSeq = 1;
}

OscillateS :: ~ OscillateS(){
	//destructor
}
//Preconditions:  None
//Postconditions: None
bool OscillateS :: Status(){
	return ObjectOn;
}
//Preconditions:  None 
//Postconditions: Can flip status is conditions are met 
void OscillateS :: UpdateStatus(){
	if (NumQueries % P == 0 && NumQueries != 0){
		NumChanges++;
		ObjectOn = !ObjectOn;
	}
	return;
}
//Preconditions: none  
//Postconditions: Will call update status
int OscillateS :: Query(int x){
	NumQueries++;
	ResetSeq = K * NumChanges;
	int next = 0;
	if (ObjectOn)
	{
		if (TotalResets(ResetSeq))
		{
			next = (CurrentValue + ArithHop);
			CurrentValue = next;
		}
		else
		{
			ResetsDone++;
			CurrentValue = Seed;
			next = CurrentValue;
		}
	}
	UpdateStatus();
	return next * MakeNeg;
}

//Preconditions: None 
//Postconditions: Will signal when there's been enough resets to get query to 
//return from the sequence, instead of just counting number of queries as Iseqs does
bool OscillateS :: TotalResets(int ResetSeq){
	bool totalComplete = true;
	if(ResetsDone == ResetSeq)
	{
		ResetSeq = 0;
		ResetsDone = 0;
		return totalComplete;
	}
	else
		return !totalComplete;	
}