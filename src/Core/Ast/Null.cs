using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class Null : IExpression
    {
        public string Inspect()
        {
            return "null";
        }
    }
}
