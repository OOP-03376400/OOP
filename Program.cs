using System;
using System.IO;

namespace p6
{
    class Program
    {

        const int ArrSize = 32;

        const int ThreeQuartersSize = ArrSize * 3 / 4;
        const int HalfSize = ArrSize / 2;
        const int WordCheck = 5;
        const int Testseq = 8;
        const int QuartersSize = ArrSize / 4;
        static readonly Random rand = new Random();



        //Preconditions: none
        //Postconditions: Introduces client to the project, basic overview of functionality etc
        static void Intro()
        {
            Console.WriteLine("This project is simulated multiple inheritance via interfaces and composition. The two major classes are Transformer and Iseq. Transformer encapsulates a string");
            Console.WriteLine("and provides the functions AsciiValue and LastChar. AsciiValue returns the ascii value of a specific character in its string, and lastchar returns that examined character.");
            Console.WriteLine("Iseq provides several functions and has 3 children classes, RandomS, OscillateS and TransformerIseq. The main functionality of the Iseq type is to encapsulate an int, then");
            Console.WriteLine("based on that value it internally triggers state change which limits what the main function, Query returns. If any of the Iseq-type objects are off they return 0, if on then");
            Console.WriteLine("Iseq returns the next number in a sequence, OscillateS returns the negative value of that same sequence, randomS returns a value from a constant random sequence. TransformerIseq");
            Console.WriteLine("encapsulates a Trasnformer and Iseq-type object to provide a different AsciiValue function. It replaces the passed in int for indexing and generates it's own via the Iseq-type object");
            Console.WriteLine("Query function. For all Iseq-type object you can also check to see if it's active, and reset it to clear all counters and make active again.");
            return;
        }

        //Preconditions: None
        //Postconditions: Instantiates each index of Iseq interface array with a different Iseqs-type object,
        //8 of each type: Regular, RandomS, OscillateS, TransformerIseq (that contains 2 Iseqs and 3 RandomS 3 OscillateS objects)
        private static IIseqs[] SeqConstructor(IIseqs[] IseqsArr)
        {
            string blank = "blank";
            for (int i = 0; i < QuartersSize; i++)
                IseqsArr[i] = new Iseqs((uint)rand.Next());
            for (int i = QuartersSize; i < HalfSize; i++)
                IseqsArr[i] = new RandomS((uint)rand.Next());
            for (int i = HalfSize; i < ThreeQuartersSize; i++)
                IseqsArr[i] = new OscillateS((uint)rand.Next());
            for (uint i = ThreeQuartersSize; i < ArrSize; i++)
                IseqsArr[i] = new TransformerIseq(blank, (uint)rand.Next(), i);
            return IseqsArr;
        }

        //Preconditions: Each index of array passed in has been initiallized
        //Postconditions: Tests each object in array to make sure that they can change state, and also follow some type of sequence
        private static void TestIseqs(IIseqs[] arr)
        {
            int value;
            for (int i = 0; i < ArrSize; i++)
            {
                Console.WriteLine("Testing Iseqs type object " + (i + 1) + ".");
                for (int j = 0; j < Testseq; j++)
                {
                    value = arr[i].Query();
                    Console.WriteLine("Query results in " + value + ".");
                }
                Console.WriteLine("Press enter to continue.\n");
                Console.ReadLine();
            }
            return;
        }

        //Preconditions: Each index of passed in array has been instantiated
        //Postconditions: Tests the reset and status functionality of the Iseq hetergenous collection.
        private static void TestResetsStatus(IIseqs[] arr)
        {
            bool active;
            int querybefore;
            int queryafter;
            for (int i = 0; i < ArrSize; i++)
            {
                active = arr[i].Status();
                if (!active)
                {
                    querybefore = arr[i].Query();
                    Console.WriteLine("Object " + (i + 1) + " is inactive. A query now should result in zero. Query results in " + querybefore + ".");
                    arr[i].Reset();
                    queryafter = arr[i].Query();
                    active = arr[i].Status();
                    if (active)
                        Console.WriteLine("Object has been reset and a query results in " + queryafter + ".");
                    else
                        Console.WriteLine("Object was not properly reset.");

                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }
            }
            return;
        }


        //Preconditions: Words must be at least as big as the transformer interface array being passed in
        //Postconditions: Instantitates each trasformer type object in array with new transformer type object
        //The first 8 are regular transformers, then the last 24 are transformerIseqs with 8 of each type of Iseq
        private static ITransformer[] TransConstructor(ITransformer[] transArr, string[] words)
        {

            for (int i = 0; i < QuartersSize; i++)
                transArr[i] = new Transformer(words[i]);
            //Since TransformerIseq automatically mods by 3 to choose Iseq, it alternates
            //Which type of iseq to generate each time i increases
            for (uint i = QuartersSize; i < ArrSize; i++)
                transArr[i] = new TransformerIseq(words[i], (uint)rand.Next(), i);
            return transArr;
        }

        //Preconditions: Passed in transformer interface array, each index of array has been instantiated
        //Postconditions: Tests the Asciivalue and Char functions to see if correct ascii value is obtained
        private static void TestTransform(ITransformer[] transArr)
        {
            char character;
            int asciiValue;
            for (int i = 0; i < ArrSize; i++)
            {
                Console.WriteLine("Testing Transformer type object " + (i + 1) + ".");
                for (int j = 0; j < WordCheck; j++)
                {
                    asciiValue = transArr[i].AsciiValue(rand.Next());
                    character = transArr[i].Char();
                    Console.WriteLine("Returned an ascii value of " + asciiValue + " for the character: " + character);
                }
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            return;
        }


        //Preconditions: All classes are open to instantiation, text file with 32 strings, 1 per line available, correct packages
        //Postconditions: Instantiates 2 heterogenous collections of Iseq type objects and transformer type objects, runs through testing 
        //all the different types and functions available to each type.
        static void Main(string[] args)
        {
            Intro();
            IIseqs[] IseqsArr = new IIseqs[ArrSize];
            ITransformer[] TransArr = new ITransformer[ArrSize];
            string example = @"C:\\Users\\fffal\\Desktop\\text.txt";
            Console.WriteLine("To initiallize Transformer, enter the string path now. AN example is as follows, but the double slashes should be single " + example + ". It must also have at least 32 strings, one string per line, \nor else it will appended the word word. Enter the path now.");
            string path = Console.ReadLine();
            string[] wordArr;

            if (File.Exists(path))
                wordArr = File.ReadAllLines(path);
            else
                wordArr = File.ReadAllLines(example);

            if (wordArr.Length < ArrSize)
            {
                int index = wordArr.Length;
                while (index < ArrSize)
                {
                    wordArr[index] = "words";
                    index++;
                }

            }
            SeqConstructor(IseqsArr);
            TestIseqs(IseqsArr);
            TestResetsStatus(IseqsArr);
            TransConstructor(TransArr, wordArr);
            TestTransform(TransArr);
            Console.ReadLine();
        }
    }
}
