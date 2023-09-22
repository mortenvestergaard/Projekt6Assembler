using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt6Assembler.Interfaces;

namespace Projekt6Assembler
{
    public class BitWriter : IBitWriter
    {
        public string ConvertToBinary(string number)
        {
            return Convert.ToString(Int32.Parse(number), 2);
        }

        public string FillZero(string binaryNumber)
        {
            for (int i = binaryNumber.Length; i < 16; i++)
            {
                binaryNumber = binaryNumber.Insert(0, "0");
            }
            return binaryNumber;
        }
    }
}
