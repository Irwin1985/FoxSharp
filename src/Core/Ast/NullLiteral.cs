using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class NullLiteral : IExpression
    {
        public string Inspect()
        {
            return "null";
        }
        public NodeType Type()
        {
            return NodeType.NULL;
        }
    }
}
