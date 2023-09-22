using Projekt6Assembler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt6Assembler
{
    //Manager for storing labels and variables, and for looking up symbol variables
    internal class VariableManager:IVariableManager
    {
        public Dictionary<string, string> SymbolTable { get; set; }
        public Dictionary<string, string> SyntaxVariables { get; set; }
        public Dictionary<string, string> Labels { get; set; }

        public VariableManager()
        {
            PopulateSymbolTable();
        }
        public void PopulateSymbolTable()
        {
            SyntaxVariables = new Dictionary<string, string>();
            Labels = new Dictionary<string, string>();
            SymbolTable = new Dictionary<string, string>()
            {
                {"R0", "0" },
                {"R1", "1" },
                {"R2", "2" },
                {"R3", "3" },
                {"R4", "4" },
                {"R5", "5" },
                {"R6", "6" },
                {"R7", "7" },
                {"R8", "8" },
                {"R9", "9" },
                {"R10", "10" },
                {"R11", "11" },
                {"R12", "12" },
                {"R13", "13" },
                {"R14", "14" },
                {"R15", "15" },
                {"SCREEN", "16384" },
                {"KBD", "24576" },
                {"SP", "0" },
                {"LCL", "1" },
                {"ARG", "2" },
                {"THIS", "3" },
                {"THAT", "4" },
                {"LOOP", "4" },
                {"STOP", "18" },
                {"END", "22" },
            };
        }

        /// <summary>
        /// Removes labels from the list of file lines so they wont get written in the output file
        /// </summary>
        /// <param name="listOfFileLines"></param>
        /// <returns></returns>
        public List<string> RemoveLabelsFromList(List<string> listOfFileLines)
        {
            //Remove all the labels from the cleaned file list of lines so its ready for binary convertion
            foreach (var label in Labels)
            {
                listOfFileLines.Remove(label.Key);
            }
            return listOfFileLines;
        }

        public string AddSyntaxVariable(string syntaxVariable)
        {

            int minSyntaxVarNumber = 16;
            int count = SyntaxVariables.Count;//Syntax variable always start at minimum index 16
            SyntaxVariables.Add(syntaxVariable, (count + minSyntaxVarNumber).ToString());
            return SyntaxVariables[syntaxVariable];
        }
    }
}
