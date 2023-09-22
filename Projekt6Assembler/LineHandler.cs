using Projekt6Assembler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekt6Assembler;

namespace Projekt6Assembler
{
    public class LineHandler : ILineHandler
    {
        public string FindCompBitValue(string compValue, Dictionary<string, string> comp)
        {
            if (comp.ContainsKey(compValue))
            {
                return comp[compValue];
            }
            else
            {
                return "000000";
            }
        }

        public string FindDestBitValue(string destValue, Dictionary<string, string> dest)
        {
            if (dest.ContainsKey(destValue))
            {
                return dest[destValue];
            }
            else
            {
                return "000";
            }
        }

        public string FindJumpBitValue(string jumpValue, Dictionary<string, string> jump)
        {
            if (jump.ContainsKey(jumpValue))
            {
                return jump[jumpValue];
            }
            else
            {
                return "000";
            }
        }
    }
}
