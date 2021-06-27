using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class BooleanLiteral : IExpression
    {
        public Token Token;
        public bool Value;
        public BooleanLiteral() { }
        public BooleanLiteral(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return Value ? "true" : "false";
        }
    }
}
