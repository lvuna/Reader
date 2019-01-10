using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class CommandList
    {
        List<String> commandList;

        public CommandList()
        {
            commandList = new List<string>();
            commandList.Add("R - start the reading mode");
            commandList.Add("A - add element to the storage");
            commandList.Add("Q - quit the program");
            commandList.Add("V - view the storage of your word");
            commandList.Add("D - delete the word from storage");
            commandList.Add("S - setting the delay between every word while reading(in miliseconds, default 1000ms = 1s)");
            commandList.Add("");
        }

        public void printList()
        {
            foreach (String command in this.commandList)
            {
                Console.WriteLine(command);
            }
        }
    }
}
