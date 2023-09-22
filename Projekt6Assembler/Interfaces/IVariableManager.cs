using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt6Assembler.Interfaces
{
    internal interface IVariableManager
    {
        public void PopulateSymbolTable();
        public List<string> RemoveLabelsFromList(List<string> fileLines);
        public string AddSyntaxVariable(string syntaxVariable);
    }
}
