//p4.cpp
//Sean Wright
//Version 1
//Implementation file for all objects

#include "Transformer.h"
#include "Iseqs.h"
#include "TransformerIseqs.h"
#include "RandomS.h"
#include "OscillateS.h"
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <ctime>

using namespace std;
const int size = 30;
const int numWords = 90;
const int RandCeiling = 30;

//Preconditions: 
//Postconditions: 
void TestIseq(Iseqs* a[]){
	int value = 0;
	for(int i = 0; i <size; i++){
		cout<<"Testing object number "<<i+1<<":"<<endl;
		for(int j = 0; j <size/3;j++){
			value = a[i]->Query(rand()%RandCeiling);
			cout<<value<<" ";
		}
		cout<<endl;
	}
	for(int i = 0; i<size;i++)
		delete a[i];
}

//Preconditions: 
//Postconditions: 
void TestTransform(Transformer* a[]){
	
	
	return;
}

//Preconditions: 
//Postconditions: 
void Introduction(){
	cout<<"This assortment of programs is an assignment ini multiple inheritance in c++."<<endl;
	cout<<"The heirarchy is there's a child class called TransformerIseqs which is composed"<<endl;
	cout<<"of a Transformer parent (autobot?), and an Iseq parent. The Iseq portion is also"<<endl;
	cout<<"a parent in the sense that Iseq has 2 child objects, RandomS and OscillateS. The"<<endl;
	cout<<"functionality of those classes are described in their respective files, I'll only"<<endl;
	cout<<"be going over the interface here. All Iseq objects can perform Query, which returns"<<endl;
	cout<<"an integer from a sequence, and Status which returns if the object is active or not."<<endl;
	cout<<"All Iseq objects encapsulate a value which controls how many queries until it becomes inactive."<<endl;
	cout<<"The interface for the Transformer class is it encapsulates a string upon creation,"<<endl;
	cout<<"and provides a function AsciiValue which takes in an int, which is mapped to a specific"<<endl;
	cout<<"character in its encapsulated string, then returns the AsciiValue of that character."<<endl;
	cout<<"The TransformerIseqs utilizes the integer generation of an Iseq type object, then uses"<<endl;
	cout<<"that int value as the input to a Transformer class. I will be making heterogenous "<<endl;
	cout<<"collections of all the different Iseq objects, query them, then I'll make another"<<endl;
	cout<<"heterogenous collection of Transformer objects, half regular Transformer type the other half"<<endl;
	cout<<"will be TransformerIseqs objects, 4 with the base Iseq type and 3  using RandomS & OscillateS type."<<endl;
}


//I wanted to use functional decomposition but
//kept on having different erro types, so I ended up 
//declaring everything locally in main and deallocating here
//The assignment also didn't mention needing functional decomp even
//though it would've made it easier to read :/

//Preconditions: 
//Postconditions: 
int main(){
	srand(time(NULL));
	ifstream inFile;
	inFile.open("lazy.txt");
	string inWord;
	string WordArr[numWords];
	int wordCount = 0;
	char ch;
	while(inFile>>inWord){
		WordArr[wordCount] = inWord;
		wordCount++;
	}
	Introduction();
	inFile.close();
	Iseqs* arr[size];
	
	//Iseqs array filled as 10 Iseqs, 10 RandomIseqs, 10 OscillateIseqs
	for(int i = 0; i<10;i++)
		arr[i] = new Iseqs(i*i);
	
	for(int i = 10; i<20; i++)
		arr[i] = new RandomS(i+i);
	for(int i = 20; i<size; i++)
		arr[i] = new OscillateS(i); 
	
	
	//This is testing Iseq arrays
	//TestIseq(arr);
	cout<<"Testing Iseq objects first. There are 30 total, 10 Iseq, 10 randomS, 10 OscillateS."<<endl;
	int value = 0;
	for(int i = 0; i <size; i++){
		cout<<"Testing object number "<<i+1<<":"<<endl;
		for(int j = 0; j <size/3;j++){
			value = arr[i]->Query(rand()%RandCeiling);
			cout<<value<<" ";
		}
		cout<<endl;
	}
	
	
	//Creating 30 Transformer objects, the last 15 are TransformerIseqs objects
	//The first parameter in TransformerIseqs is which type of Iseq: 0 = normal, 1= randomS, 2 = OscillateS
	cout<<"Testing Transformer type objects, the first 15 are Transformer objects, the next 15 are TransformerIseqs."<<endl;
	cout<<"For the transformIseq it creates in this order; Transformer+ Iseq, Transformer + RandomS, Transformer+ OscillateS"<<endl;
	Transformer* Transarr[size];
	for(int i = 0; i < 15;i++)
		Transarr[i] = new Transformer(WordArr[i]);
	for(int i = 15; i<size; i++)
		Transarr[i] = new TransformerIseqs((i%2),i,WordArr[i]);
	for(int i = 0; i<size;i++){
		cout<<"Testing object number "<<i+1<<":\n";
		for(int j = 0; j <size/5;j++){
			value = Transarr[i]->AsciiValue(j,ch);
			cout<<"The ascii value of "<<ch<<" is " <<value<<"."<<endl;			
		}
		cout<<endl;
	}
	
	
	//Deallocate all the heap objects
	for(int i = 0; i < size; i++){
		delete arr[i];
		delete Transarr[i];		
	}
	
	return 0;
}