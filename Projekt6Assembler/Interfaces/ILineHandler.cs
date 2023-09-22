using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt6Assembler.Interfaces
{
    public interface ILineHandler
    {
        public string FindCompBitValue(string compValue, Dictionary<string, string> comp);
        public string FindDestBitValue(string destValue, Dictionary<string, string> dest);
        public string FindJumpBitValue(string jumpValue, Dictionary<string, string> jump);
    }
}
