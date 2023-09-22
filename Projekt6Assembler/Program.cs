using Projekt6Assembler.Interfaces;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;

namespace Projekt6Assembler
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Paths. Should be stored somewhere else
            string path = "C:\\Users\\mort286f\\Desktop\\nand2tetris\\nand2tetris\\projects\\06\\pong\\Pong.asm";
            string destinationFile = "C:\\Users\\mort286f\\Desktop\\nand2tetris\\nand2tetris\\projects\\06\\pong\\Output.txt";

            //Instantiation. Should be injected through a contructor
            RegisterManager registerManager = new RegisterManager();
            IBitWriter bitWriter = new BitWriter();
            VariableManager variableManager = new VariableManager();
            FileReader fileReader = new FileReader();
            ILineHandler lineHandler = new LineHandler();


            string file = fileReader.ReadAsmFile(path);
            string cleanedFileLines = fileReader.RemoveCodeCommentsAndSpaces(file);
            List<string> cleanFileLines = cleanedFileLines.Split('\n').ToList(); //Contains all lines without spaces or code comments

            string indexBits;
            string finalBitValue = ""; //A combination of concatenated string depending on the current line being read
            List<string> finalBitValueList = new List<string>();
            for (int i = 0; i < cleanFileLines.Count; i++)
            {
                if (cleanFileLines[i].Contains("("))
                {
                    variableManager.Labels.Add(cleanFileLines[i].Trim(new char[] { '(', ')' }), (i - variableManager.Labels.Count).ToString());
                    string label = cleanFileLines.ElementAt(i);
                    label = label.Trim(new char[] { '(', ')' });
                    cleanFileLines[i] = label;

                }
            }

            List<string> cleanLinesList = variableManager.RemoveLabelsFromList(cleanFileLines);

            for (int i = 0; i < cleanLinesList.Count; i++)
            {
                //Check if line is a variable
                if (cleanLinesList[i].Contains("@"))
                {
                    string trimmedLine = cleanLinesList[i].Substring(1);

                    bool isLabel = variableManager.Labels.ContainsKey(trimmedLine); //If the variable is a label
                    if (isLabel)
                    {
                        string labelValue = variableManager.Labels[trimmedLine];
                        finalBitValue = bitWriter.ConvertToBinary(labelValue);
                    }
                    else
                    {

                        if (variableManager.SymbolTable.ContainsKey(trimmedLine)) //Is the variable in the symbol table?
                        {
                            string symbolTableValue = variableManager.SymbolTable[trimmedLine];
                            finalBitValue = bitWriter.ConvertToBinary(symbolTableValue);
                        }

                        var isNumeric = int.TryParse(trimmedLine, out _); //Is the variable a syntax variable?
                        if (!isNumeric)
                        {
                            if (!variableManager.SyntaxVariables.ContainsKey(trimmedLine)) //If the variable has not already been defined
                            {
                                if (!variableManager.SymbolTable.ContainsKey(trimmedLine))
                                {
                                    string addedVariable = variableManager.AddSyntaxVariable(trimmedLine);
                                    finalBitValue = bitWriter.ConvertToBinary(addedVariable);
                                }
                            }
                            else if (variableManager.SyntaxVariables.ContainsKey(trimmedLine)) //If the syntax variable already exists, return its index value
                            {
                                string syntaxVariable = variableManager.SyntaxVariables[trimmedLine];
                                finalBitValue = bitWriter.ConvertToBinary(syntaxVariable);
                            }
                        }
                        else if (isNumeric)
                        {
                            finalBitValue = bitWriter.ConvertToBinary(trimmedLine);
                        }

                    }
                    finalBitValue = bitWriter.FillZero(finalBitValue);
                }

                else if (cleanLinesList[i].Contains("="))
                {
                    indexBits = "111";
                    string compValue = cleanLinesList[i].Substring(cleanLinesList[i].IndexOf("=") + 1);
                    string compBitValue = lineHandler.FindCompBitValue(compValue, registerManager.Compositions);

                    string destValue = cleanLinesList[i].Split("=")[0];
                    string destBitValue = lineHandler.FindDestBitValue(destValue, registerManager.Destinations);


                    if (cleanFileLines[i].Contains(";")) //If the line contains a jump
                    {
                        string jumpValue = cleanLinesList[i].Substring(cleanLinesList[i].IndexOf(";") + 1);
                        string jumpBitValue = lineHandler.FindJumpBitValue(jumpValue, registerManager.Jumps);
                        //indexBits.Concat(compBitValue).Concat(destBitValue).Concat(jumpBitValue);
                        finalBitValue = indexBits + compBitValue + destBitValue + jumpBitValue;
                    }
                    else
                    {
                        finalBitValue = indexBits + compBitValue + destBitValue + "000";
                    }

                }

                else if (cleanFileLines[i].Contains(";")) //If the line contains a jump
                {
                    indexBits = "111";
                    string compValue = "0";
                    string destValue = "0";
                    string compBitValue;
                    string destBitValue;
                    string jumpValue = cleanFileLines[i].Substring(cleanFileLines[i].IndexOf(";") + 1);

                    if (cleanFileLines[i].Contains("=")) //If the line also has a destination
                    {
                        compValue = cleanFileLines[i].Substring(cleanFileLines[i].IndexOf("=") + 1);
                        compBitValue = lineHandler.FindCompBitValue(compValue, registerManager.Compositions);
                        destValue = cleanFileLines[i].Split(";")[0];
                        destBitValue = lineHandler.FindDestBitValue(destValue, registerManager.Destinations);
                    }
                    else
                    {
                        compValue = cleanFileLines[i].Split(";")[0];
                        compBitValue = lineHandler.FindCompBitValue(compValue, registerManager.Compositions);
                        destBitValue = lineHandler.FindDestBitValue(destValue, registerManager.Destinations);
                    }
                    string jumpBitValue = lineHandler.FindJumpBitValue(jumpValue, registerManager.Jumps);
                    finalBitValue = indexBits + compBitValue + destBitValue + jumpBitValue;
                }

                else if (cleanFileLines[i].Contains("(")) //If the line is a variable, continue
                {
                    continue;
                }

                Console.WriteLine(finalBitValue);
                finalBitValueList.Add(finalBitValue);
            }
            //close the file

            File.WriteAllLines(destinationFile, finalBitValueList);
            Console.ReadLine();
        }
    }
}