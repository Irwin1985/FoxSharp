using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxSharp
{
    class Identifier : IExpression
    {
        public Token Token;
        public string Value;
        public Identifier() { }
        public Identifier(Token token)
        {
            this.Token = token;
        }
        public string Inspect()
        {
            return Value;
        }
    }
}
