using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Patrick Reader!");
            List<String> WordList;
            CommandList command = new CommandList();
            int milliseconds = 1000;

            String path = "C:\\Downloads\\Reader.txt";
            try
            {
                WordList = ReadFromBinaryFile<List<String>>(path);
            }
            catch(FileNotFoundException e)
            {
                WordList = new List<string>();
            }

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = 0;     // -10...10
            synthesizer.Speak("Welcome to Patrick Reader!");
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                String input = Console.ReadLine();
                if (input.Equals("Q"))
                {
                    break;
                }
                else if (input.Equals("C"))
                {
                    command.printList();
                }
                else if (input.Equals("A"))
                {
                    while (true)
                    {
                        Console.WriteLine("Type in the word you would like to store or quit: ");
                        String inputWord = Console.ReadLine();
                        if (inputWord.Equals("Q"))
                            break;
                        if (!WordList.Contains(inputWord))
                        {
                            WordList.Add(inputWord);
                            WriteToBinaryFile<List<String>>(path, WordList);
                        }
                    }
                    Console.WriteLine("");
                }
                else if (input.Equals("V"))
                {
                    foreach (String word in WordList)
                    {
                        Console.WriteLine(word);
                    }
                    Console.WriteLine("");
                }
                else if (input.Equals("S"))
                {
                    Console.WriteLine("Enter the delay you wish(in miliseconds),default is 1000ms = 1s");
                    Console.WriteLine("For mysterious reason 2000 = 2s is not working properly, but 3s or above is");
                    if (Int32.TryParse(Console.ReadLine(), out int newDelay))
                        milliseconds = newDelay;
                    Console.WriteLine("");
                }
                else if (input.Equals("D"))
                {
                    while (true)
                    {
                        Console.WriteLine("Enter the Word you wish to delete or Quit: ");
                        String wordToBeDeleted = Console.ReadLine();
                        if (wordToBeDeleted.Equals("Q"))
                            break;
                        if (WordList.Contains(wordToBeDeleted))
                        {
                            WordList.Remove(wordToBeDeleted);
                        }
                    }
                    Console.WriteLine("");
                }
                else if (input.Equals("R"))
                {
                    Console.WriteLine("Enter the number of element you wish to read: ");
                    int numElement = 0;
                    Int32.TryParse(Console.ReadLine(), out numElement);
                    while (numElement > WordList.Count | numElement < 0)
                    {
                        Console.WriteLine("The number is bigger than the stored List!");
                        Console.WriteLine("Enter the number of element you wish to read: ");
                        Int32.TryParse(Console.ReadLine(), out numElement);
                    }
                    Random rnd = new Random();
                    List<String> readList = new List<string>();
                    while (readList.Count != numElement)
                    {
                        int randomNum = rnd.Next(WordList.Count);
                        if (!readList.Contains(WordList[randomNum]))
                        {
                            readList.Add(WordList[randomNum]);
                        }
                    }
                    foreach (String word in readList)
                    {
                        synthesizer.Speak(word);
                        Thread.Sleep(milliseconds);
                    }
                    Console.WriteLine("");
                }
                // Synchronous

            }
            WriteToBinaryFile<List<String>>(path, WordList);
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the XML.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

    }

}
