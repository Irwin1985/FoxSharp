using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    public class BooleanLiteral : IExpression
    {
        public Token Token;
        public bool Value;
        public BooleanLiteral() { }
        public BooleanLiteral(Token token, bool value)
        {
            this.Token = token;
            this.Value = value;
        }
        public string Inspect()
        {
            return Value ? "true" : "false";
        }
    }
}
