using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt6Assembler.Interfaces
{
    internal interface IBitWriter
    {
        public string FillZero(string binaryNumber);
        public string ConvertToBinary(string number);

    }
}
