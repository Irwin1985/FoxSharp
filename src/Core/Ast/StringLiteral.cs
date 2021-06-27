using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class StringLiteral : IExpression
    {
        public Token Token;
        public string Value;
        public StringLiteral() { }
        public StringLiteral(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return Value;
        }
    }
}
