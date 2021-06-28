using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class IntegerLiteral : IExpression
    {
        public Token Token;
        public int Value;
        public IntegerLiteral() { }
        public IntegerLiteral(Token token, int value)
        {
            this.Token = token;
            this.Value = value;
        }
        public String Inspect()
        {
            return Value.ToString();
        }
    }
}
