using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class IntegerLiteral : IExpression
    {
        public Token Token;
        public int Value;
        public IntegerLiteral() { }
        public IntegerLiteral(Token token)
        {
            this.Token = token;
        }
        public String Inspect()
        {
            return Value.ToString();
        }
    }
}
